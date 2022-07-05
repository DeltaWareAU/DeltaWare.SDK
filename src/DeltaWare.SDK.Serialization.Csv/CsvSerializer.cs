using DeltaWare.SDK.Core.Caching;
using DeltaWare.SDK.Core.Helpers;
using DeltaWare.SDK.Serialization.Csv.Attributes;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using DeltaWare.SDK.Serialization.Csv.Header;
using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Settings;
using DeltaWare.SDK.Serialization.Csv.Validation;
using DeltaWare.SDK.Serialization.Csv.Writing;
using DeltaWare.SDK.Serialization.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    public class CsvSerializer : ICsvSerializer
    {
        private readonly IAttributeCache _attributeCache;
        private readonly DefaultHeaderHandler _headerHandler;
        private readonly IObjectSerializer _objectSerializer;
        private readonly ICsvValidator _csvValidator;
        private readonly ICsvSerializerSettings _settings;

        public CsvSerializer(ICsvSerializerSettings settings = null)
        {
            _settings = settings ?? CsvSerializerSettings.Default;

            _attributeCache = settings?.AttributeCache ?? new AttributeCache();
            _objectSerializer = settings?.Serializer ?? new ObjectSerializer(_attributeCache);
            _csvValidator = settings?.Validator ?? new DefaultCsvValidator(_attributeCache);

            _headerHandler = new DefaultHeaderHandler(_attributeCache, _objectSerializer);
        }

        public CsvSerializer(Action<CsvSerializerSettings> settingsBuilder)
        {
            CsvSerializerSettings settings = CsvSerializerSettings.Default;

            settingsBuilder.Invoke(settings);

            _settings = settings;

            _attributeCache = settings?.AttributeCache ?? new AttributeCache();
            _objectSerializer = settings?.Serializer ?? new ObjectSerializer(_attributeCache);
            _csvValidator = settings?.Validator ?? new DefaultCsvValidator(_attributeCache);

            _headerHandler = new DefaultHeaderHandler(_attributeCache, _objectSerializer);
        }

        #region Async Methods

        public async Task<object> DeserializeRecordsAsync(CsvReader reader, Type containerSchema)
        {
            object container = Activator.CreateInstance(containerSchema);

            await DeserializeRecordsAsync(reader, container);

            return container;
        }

        public async Task DeserializeRecordsAsync(CsvReader reader, object container)
        {
            Type containerSchema = container.GetType();

            PropertyInfo[] publicProperties = containerSchema.GetPublicProperties();

            Dictionary<Type, PropertyInfo> recordSchemas = new Dictionary<Type, PropertyInfo>();

            foreach (PropertyInfo property in publicProperties)
            {
                if (!property.PropertyType.ImplementsInterface<IList>())
                {
                    // Non IList types are not supported.
                    continue;
                }

                Type genericType = property.PropertyType.GetGenericArguments().First();

                if (!_attributeCache.HasAttribute<RecordTypeAttribute>(genericType))
                {
                    continue;
                }

                recordSchemas.Add(genericType, property);
            }

            if (recordSchemas.IsEmpty())
            {
                throw new Exception("No record schemas found for container");
            }

            IEnumerable<object> records = await DeserializeRecordsAsync(reader, recordSchemas.Keys.ToArray());

            IEnumerable<IGrouping<Type, object>> recordTypeGroup = records.GroupBy(r => r.GetType());

            foreach (IGrouping<Type, object> recordType in recordTypeGroup)
            {
                if (!recordSchemas.TryGetValue(recordType.Key, out PropertyInfo property))
                {
                    if (_settings.IgnoreUnknownRecords)
                    {
                        continue;
                    }

                    throw CsvSchemaException.UndeclaredRecordTypeEncountered(recordType.Key.Name);
                }

                IList list = (IList)property.GetValue(container);

                if (list == null)
                {
                    list = GenericTypeHelper.CreateGenericList(recordType.Key);

                    property.SetValue(container, list);
                }

                list.AddRange(recordType);
            }
        }

        public async Task<IEnumerable<object>> DeserializeRecordsAsync(CsvReader reader, params Type[] schema)
        {
            List<object> deserializedValues = new List<object>();

            Dictionary<string, Type> recordSchemaTypeMap = BuildRecordSchemaTypeMap(schema);

            Dictionary<string, PropertyInfo[]> indexedRecordProperties = new Dictionary<string, PropertyInfo[]>();

            while (!reader.EndOfStream)
            {
                string[] currentLine = await reader.ReadLineAsync();

                string recordType = currentLine[0];

                if (!recordSchemaTypeMap.TryGetValue(recordType, out Type recordSchema))
                {
                    if (_settings.IgnoreUnknownRecords)
                    {
                        continue;
                    }

                    throw CsvSchemaException.UndeclaredRecordTypeEncountered(recordType);
                }

                if (!indexedRecordProperties.TryGetValue(recordType, out PropertyInfo[] indexedProperties))
                {
                    indexedProperties = _headerHandler.BuildHeaderIndex(recordSchema, currentLine.Length - 1);

                    indexedRecordProperties.Add(recordType, indexedProperties);
                }

                object deserializedValue = DeserializeLine(currentLine.Skip(1).ToArray(), recordSchema, indexedProperties);

                deserializedValues.Add(deserializedValue);
            }

            return deserializedValues;
        }

        /// <inheritdoc cref="ICsvSerializer.DeserializeAsync"/>
        public async Task<IEnumerable<object>> DeserializeAsync(CsvReader reader, Type schema, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(schema, out HeaderRequiredAttribute _))
            {
                hasHeaders = true;
            }

            bool firstLine = true;

            PropertyInfo[] indexedProperties = null;

            List<object> deserializedValues = new List<object>();

            while (!reader.EndOfStream)
            {
                string[] currentLine = await reader.ReadLineAsync();

                if (firstLine)
                {
                    firstLine = false;

                    if (hasHeaders)
                    {
                        indexedProperties = _headerHandler.BuildHeaderIndex(schema, currentLine.Length, currentLine);

                        continue;
                    }

                    indexedProperties = _headerHandler.BuildHeaderIndex(schema, currentLine.Length);
                }

                object deserializedValue = DeserializeLine(currentLine, schema, indexedProperties);

                deserializedValues.Add(deserializedValue);
            }

            return deserializedValues;
        }

        /// <inheritdoc cref="ICsvSerializer.SerializeAsync{T}"/>
        public async Task SerializeAsync<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class
        {
            PropertyInfo[] indexedProperties = _headerHandler.GetIndexedProperties(typeof(T));

            string[] header = _headerHandler.GetHeaderNames(indexedProperties);

            await writer.WriteLineAsync(header);

            foreach (object line in lines)
            {
                string[] fields = SerializeLine(line, indexedProperties);

                await writer.WriteLineAsync(fields);
            }

            await writer.FlushAsync();
        }

        #endregion

        #region Sync Methods

        /// <inheritdoc cref="ICsvSerializer.Deserialize"/>
        public IEnumerable<object> Deserialize(CsvReader reader, Type schema, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(schema, out HeaderRequiredAttribute _))
            {
                hasHeaders = true;
            }

            bool firstLine = true;

            PropertyInfo[] indexedProperties = null;

            while (!reader.EndOfStream)
            {
                string[] currentLine = reader.ReadLine();

                if (firstLine)
                {
                    firstLine = false;

                    if (hasHeaders)
                    {
                        indexedProperties = _headerHandler.BuildHeaderIndex(schema, currentLine.Length, currentLine);

                        continue;
                    }

                    indexedProperties = _headerHandler.BuildHeaderIndex(schema, currentLine.Length);
                }

                yield return DeserializeLine(currentLine, schema, indexedProperties);
            }
        }

        /// <inheritdoc cref="ICsvSerializer.Serialize{T}"/>
        public void Serialize<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class
        {
            PropertyInfo[] propertyIndex = _headerHandler.GetIndexedProperties(typeof(T));

            string[] header = _headerHandler.GetHeaderNames(propertyIndex);

            writer.WriteLine(header);

            foreach (object line in lines)
            {
                string[] fields = SerializeLine(line, propertyIndex);

                writer.WriteLine(fields);
            }

            writer.Flush();
        }

        #endregion

        private Dictionary<string, Type> BuildRecordSchemaTypeMap(Type[] schema)
        {
            Dictionary<string, Type> recordTypeMap = new Dictionary<string, Type>();

            foreach (Type recordSchema in schema)
            {
                if (!_attributeCache.TryGetAttribute(recordSchema, out RecordTypeAttribute recordType))
                {
                    throw CsvSchemaException.InvalidRecordTypeDeclaration(recordSchema);
                }

                recordTypeMap.Add(recordType.Type, recordSchema);
            }

            return recordTypeMap;
        }

        private object DeserializeLine(string[] line, Type schema, PropertyInfo[] indexedProperties)
        {
            object parentObject = Activator.CreateInstance(schema);

            for (int j = 0; j < line.Length; j++)
            {
                PropertyInfo property = indexedProperties[j];

                string value = line[j];

                object childObject = _objectSerializer.Deserialize(value, property);

                _csvValidator.Validate(childObject, property);

                property.SetValue(parentObject, childObject);
            }

            return parentObject;
        }

        private string[] SerializeLine(object line, PropertyInfo[] indexedProperties)
        {
            string[] fields = new string[indexedProperties.Length];

            for (int i = 0; i < indexedProperties.Length; i++)
            {
                object childValue = indexedProperties[i].GetValue(line);

                _csvValidator.Validate(childValue, indexedProperties[i]);

                string field = _objectSerializer.Serialize(childValue, indexedProperties[i]);

                fields[i] = field;
            }

            return fields;
        }
    }
}