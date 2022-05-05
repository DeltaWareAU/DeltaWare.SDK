using DeltaWare.SDK.Core.Caching;
using DeltaWare.SDK.Serialization.Csv.Attributes;
using DeltaWare.SDK.Serialization.Csv.Header;
using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Writing;
using DeltaWare.SDK.Serialization.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    public class CsvSerializer : ICsvSerializer
    {
        private readonly IAttributeCache _attributeCache;
        private readonly HeaderHandler _headerHandler;
        private readonly IObjectSerializer _objectSerializer;

        public CsvSerializer(IObjectSerializer objectSerializer = null)
        {
            _attributeCache = new AttributeCache();
            _objectSerializer = objectSerializer ?? new ObjectSerializer(_attributeCache);

            _headerHandler = new HeaderHandler(_attributeCache, _objectSerializer);
        }

        #region Async Methods

        /// <inheritdoc cref="ICsvSerializer.DeserializeAsync"/>
        public async Task<IEnumerable<object>> DeserializeAsync(CsvReader reader, Type schema, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(schema, out HeaderRequiredAttribute _))
            {
                hasHeaders = true;
            }

            bool firstLine = true;

            PropertyInfo[] propertyIndex = null;

            List<object> deserializedValues = new List<object>();

            while (!reader.EndOfStream)
            {
                string[] currentLine = await reader.ReadLineAsync();

                if (firstLine)
                {
                    firstLine = false;

                    if (hasHeaders)
                    {
                        propertyIndex = _headerHandler.BuildHeaderIndex(schema, currentLine.Length, currentLine);

                        continue;
                    }

                    propertyIndex = _headerHandler.BuildHeaderIndex(schema, currentLine.Length);
                }

                object parentObject = Activator.CreateInstance(schema);

                for (int j = 0; j < currentLine.Length; j++)
                {
                    PropertyInfo property = propertyIndex[j];

                    string value = currentLine[j];

                    object childObject = _objectSerializer.Deserialize(value, property);

                    property.SetValue(parentObject, childObject);
                }

                deserializedValues.Add(parentObject);
            }

            return deserializedValues;
        }

        /// <inheritdoc cref="ICsvSerializer.SerializeAsync{T}"/>
        public async Task SerializeAsync<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class
        {
            PropertyInfo[] propertyIndex = _headerHandler.GetOrderedProperties(typeof(T));

            string[] header = _headerHandler.BuilderHeader(propertyIndex);

            await writer.WriteLineAsync(header);

            foreach (object line in lines)
            {
                string[] fields = new string[propertyIndex.Length];

                for (int i = 0; i < propertyIndex.Length; i++)
                {
                    object childValue = propertyIndex[i].GetValue(line);

                    string field = _objectSerializer.Serialize(childValue, propertyIndex[i]);

                    fields[i] = field;
                }

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

            PropertyInfo[] propertyIndex = null;

            while (!reader.EndOfStream)
            {
                string[] currentLine = reader.ReadLine();

                if (firstLine)
                {
                    firstLine = false;

                    if (hasHeaders)
                    {
                        propertyIndex = _headerHandler.BuildHeaderIndex(schema, currentLine.Length, currentLine);

                        continue;
                    }

                    propertyIndex = _headerHandler.BuildHeaderIndex(schema, currentLine.Length);
                }

                object parentObject = Activator.CreateInstance(schema);

                for (int j = 0; j < currentLine.Length; j++)
                {
                    PropertyInfo property = propertyIndex[j];

                    string value = currentLine[j];

                    object childObject = _objectSerializer.Deserialize(value, property);

                    property.SetValue(parentObject, childObject);
                }

                yield return parentObject;
            }
        }

        /// <inheritdoc cref="ICsvSerializer.Serialize{T}"/>
        public void Serialize<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class
        {
            PropertyInfo[] propertyIndex = _headerHandler.GetOrderedProperties(typeof(T));

            string[] header = _headerHandler.BuilderHeader(propertyIndex);

            writer.WriteLine(header);

            foreach (object line in lines)
            {
                string[] fields = new string[propertyIndex.Length];

                for (int i = 0; i < propertyIndex.Length; i++)
                {
                    object childValue = propertyIndex[i].GetValue(line);

                    string field = _objectSerializer.Serialize(childValue, propertyIndex[i]);

                    fields[i] = field;
                }

                writer.WriteLine(fields);
            }

            writer.Flush();
        }

        #endregion
    }
}