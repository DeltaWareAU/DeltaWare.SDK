using DeltaWare.SDK.Core.Caching;
using DeltaWare.SDK.Core.Helpers;
using DeltaWare.SDK.Serialization.Csv.Attributes;
using DeltaWare.SDK.Serialization.Csv.Enums;
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

        #region Deserialize Records

        public async Task<object> DeserializeRecordsAsync(ICsvReader reader, Type containerType)
        {
            object container = Activator.CreateInstance(containerType);

            await DeserializeRecordsAsync(reader, container);

            return container;
        }

        public object DeserializeRecords(ICsvReader reader, Type containerType)
        {
            object container = Activator.CreateInstance(containerType);

            DeserializeRecords(reader, container);

            return container;
        }

        public async Task DeserializeRecordsAsync(ICsvReader reader, object container)
        {
            Dictionary<Type, PropertyInfo> recordSchemas = GetContainerSchemas(container.GetType());

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

        public void DeserializeRecords(ICsvReader reader, object container)
        {
            Dictionary<Type, PropertyInfo> recordSchemas = GetContainerSchemas(container.GetType());

            IEnumerable<object> records = DeserializeRecords(reader, recordSchemas.Keys.ToArray());

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

        public async Task<IEnumerable<object>> DeserializeRecordsAsync(ICsvReader reader, params Type[] recordTypes)
        {
            List<object> deserializedValues = new List<object>();

            Dictionary<string, Type> recordSchemaTypeMap = BuildRecordSchemaTypeMap(recordTypes);

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

        public IEnumerable<object> DeserializeRecords(ICsvReader reader, params Type[] recordTypes)
        {
            List<object> deserializedValues = new List<object>();

            Dictionary<string, Type> recordSchemaTypeMap = BuildRecordSchemaTypeMap(recordTypes);

            Dictionary<string, PropertyInfo[]> indexedRecordProperties = new Dictionary<string, PropertyInfo[]>();

            while (!reader.EndOfStream)
            {
                string[] currentLine = reader.ReadLine();

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

        #endregion

        #region Serialize Records

        public async Task SerializeRecordAsync(IEnumerable<object> lines, ICsvWriter writer)
        {
            if (writer.Mode != CsvType.Record)
            {
                throw new InvalidOperationException("Invalid CSV Writer Mode, the mode must be set as Record");
            }

            Dictionary<Type, PropertyInfo[]> indexedRecordProperties = new Dictionary<Type, PropertyInfo[]>();

            foreach (object line in lines)
            {
                Type schemaType = line.GetType();

                if (!indexedRecordProperties.TryGetValue(schemaType, out PropertyInfo[] indexedProperties))
                {
                    indexedProperties = _headerHandler.GetIndexedProperties(schemaType);

                    indexedRecordProperties.Add(schemaType, indexedProperties);
                }

                if (!_attributeCache.TryGetAttribute(schemaType, out RecordTypeAttribute recordType))
                {
                    if (_settings.IgnoreUnknownRecords)
                    {
                        continue;
                    }

                    throw CsvSchemaException.InvalidRecordTypeDeclaration(schemaType);
                }

                await writer.WriteAsync(recordType.Type);

                string[] fields = SerializeLine(line, indexedProperties);

                await writer.WriteLineAsync(fields);
            }

            await writer.FlushAsync();
        }

        public void SerializeRecord(IEnumerable<object> lines, ICsvWriter writer)
        {
            if (writer.Mode != CsvType.Record)
            {
                throw new InvalidOperationException("Invalid CSV Writer Mode, the mode must be set as Record");
            }

            Dictionary<Type, PropertyInfo[]> indexedRecordProperties = new Dictionary<Type, PropertyInfo[]>();

            foreach (object line in lines)
            {
                Type schemaType = line.GetType();

                if (!indexedRecordProperties.TryGetValue(schemaType, out PropertyInfo[] indexedProperties))
                {
                    indexedProperties = _headerHandler.GetIndexedProperties(schemaType);

                    indexedRecordProperties.Add(schemaType, indexedProperties);
                }

                if (!_attributeCache.TryGetAttribute(schemaType, out RecordTypeAttribute recordType))
                {
                    if (_settings.IgnoreUnknownRecords)
                    {
                        continue;
                    }

                    throw CsvSchemaException.InvalidRecordTypeDeclaration(schemaType);
                }

                writer.Write(recordType.Type);

                string[] fields = SerializeLine(line, indexedProperties);

                writer.WriteLine(fields);
            }

            writer.Flush();
        }

        public Task SerializeRecordAsync(object container, ICsvWriter writer)
        {
            Dictionary<Type, PropertyInfo> recordSchemas = GetContainerSchemas(container.GetType());

            IEnumerable<object> lines = GetContainerValues(container, recordSchemas);

            return SerializeRecordAsync(lines, writer);
        }

        public void SerializeRecord(object container, ICsvWriter writer)
        {
            Dictionary<Type, PropertyInfo> recordSchemas = GetContainerSchemas(container.GetType());

            IEnumerable<object> lines = GetContainerValues(container, recordSchemas);

            SerializeRecord(lines, writer);
        }

        #endregion

        #region Async Methods



        /// <inheritdoc cref="ICsvSerializer.DeserializeAsync"/>
        public async Task<IEnumerable<object>> DeserializeAsync(ICsvReader reader, Type type, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(type, out HeaderRequiredAttribute _))
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
                        indexedProperties = _headerHandler.BuildHeaderIndex(type, currentLine.Length, currentLine);

                        continue;
                    }

                    indexedProperties = _headerHandler.BuildHeaderIndex(type, currentLine.Length);
                }

                object deserializedValue = DeserializeLine(currentLine, type, indexedProperties);

                deserializedValues.Add(deserializedValue);
            }

            return deserializedValues;
        }

        /// <inheritdoc cref="ICsvSerializer.SerializeAsync{T}"/>
        public async Task SerializeAsync<T>(IEnumerable<T> lines, ICsvWriter writer, bool hasHeaders = true) where T : class
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
        public IEnumerable<object> Deserialize(ICsvReader reader, Type type, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(type, out HeaderRequiredAttribute _))
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
                        indexedProperties = _headerHandler.BuildHeaderIndex(type, currentLine.Length, currentLine);

                        continue;
                    }

                    indexedProperties = _headerHandler.BuildHeaderIndex(type, currentLine.Length);
                }

                yield return DeserializeLine(currentLine, type, indexedProperties);
            }
        }

        /// <inheritdoc cref="ICsvSerializer.Serialize{T}"/>
        public void Serialize<T>(IEnumerable<T> lines, ICsvWriter writer, bool hasHeaders = true) where T : class
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

        private Dictionary<string, Type> BuildRecordSchemaTypeMap(Type[] recordTypes)
        {
            Dictionary<string, Type> recordTypeMap = new Dictionary<string, Type>();

            foreach (Type recordSchema in recordTypes)
            {
                if (!_attributeCache.TryGetAttribute(recordSchema, out RecordTypeAttribute recordType))
                {
                    throw CsvSchemaException.InvalidRecordTypeDeclaration(recordSchema);
                }

                recordTypeMap.Add(recordType.Type, recordSchema);
            }

            return recordTypeMap;
        }

        private object DeserializeLine(string[] line, Type type, PropertyInfo[] indexedProperties)
        {
            object parentObject = Activator.CreateInstance(type);

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

                fields[i] = _objectSerializer.Serialize(childValue, indexedProperties[i]);
            }

            return fields;
        }

        private Dictionary<Type, PropertyInfo> GetContainerSchemas(Type containerType)
        {
            PropertyInfo[] publicProperties = containerType.GetPublicProperties();

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

            return recordSchemas;
        }

        private IEnumerable<object> GetContainerValues(object container, Dictionary<Type, PropertyInfo> recordSchemas)
        {
            List<object> lines = new List<object>();

            foreach ((_, PropertyInfo property) in recordSchemas)
            {
                IList list = (IList)property.GetValue(container);

                lines.AddRange(list);
            }

            return lines;
        }
    }
}