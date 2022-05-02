using DeltaWare.SDK.Serialization.Csv.Attributes;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvReader = DeltaWare.SDK.Serialization.Csv.Reading.CsvReader;

namespace DeltaWare.SDK.Serialization.Csv
{
    public partial class CsvSerializer : ICsvSerializer
    {
        public IEnumerable<T> Deserialize<T>(string value, bool hasHeaders = true) where T : class
        {
            using CsvReader csvReader = new CsvReader(value);

            IEnumerable<object> values = Deserialize(typeof(T), csvReader, hasHeaders);

            return values.Cast<T>().ToArray();
        }

        public IEnumerable<T> Deserialize<T>(Stream stream, bool hasHeaders = true) where T : class
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            IEnumerable<object> values = Deserialize(typeof(T), csvReader, hasHeaders);

            return values.Cast<T>();
        }

        public IEnumerable<T> Deserialize<T>(CsvReader reader, bool hasHeaders = true) where T : class
        {
            IEnumerable<object> values = Deserialize(typeof(T), reader, hasHeaders);

            return values.Cast<T>();
        }

        public IEnumerable<object> Deserialize(Type type, CsvReader reader, bool hasHeaders = true)
        {
            string[][] lines = reader.ReadToEnd();

            object[] parentObjects;

            if (hasHeaders || _attributeCache.TryGetAttribute(type, out HeaderRequiredAttribute _))
            {
                hasHeaders = true;

                parentObjects = new object[lines.Length - 1];
            }
            else
            {
                parentObjects = new object[lines.Length];
            }

            PropertyInfo[] indexedProperties = null;

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == 0)
                {
                    if (hasHeaders)
                    {
                        indexedProperties = _headerHandler.BuildHeaderIndex(type, lines[i].Length, lines[i]);

                        continue;
                    }

                    indexedProperties = _headerHandler.BuildHeaderIndex(type, lines[i].Length);
                }

                object parentObject = Activator.CreateInstance(type);

                for (int j = 0; j < lines[i].Length; j++)
                {
                    PropertyInfo property = indexedProperties[j];

                    string value = lines[i][j];

                    object childObject = _objectSerializer.Deserialize(value, property);

                    property.SetValue(parentObject, childObject);
                }

                if (hasHeaders)
                {
                    parentObjects[i - 1] = parentObject;
                }
                else
                {
                    parentObjects[i] = parentObject;
                }
            }

            return parentObjects.AsEnumerable();
        }

        public string Serialize<T>(IEnumerable<T> lines, bool hasHeader = true) where T : class
        {
            using Stream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            Serialize(typeof(T), lines.Cast<object>(), csvWriter, hasHeader);

            stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public void Serialize<T>(IEnumerable<T> lines, Stream stream, bool hasHeader = true) where T : class
        {
            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            Serialize(typeof(T), lines.Cast<object>(), csvWriter, hasHeader);
        }

        public void Serialize<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeader = true) where T : class
        {
            Serialize(typeof(T), lines.Cast<object>(), writer, hasHeader);
        }

        public void Serialize(Type type, IEnumerable<object> lines, CsvWriter writer, bool hasHeaders = true)
        {
            PropertyInfo[] properties = _headerHandler.GetOrderedProperties(type);

            string[] header = _headerHandler.BuilderHeader(properties);

            writer.WriteLine(header);

            foreach (object line in lines)
            {
                string[] fields = new string[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    object childValue = properties[i].GetValue(line);

                    string field = _objectSerializer.Serialize(childValue, properties[i]);

                    fields[i] = field;
                }

                writer.WriteLine(fields);
            }

            writer.Flush();
        }
    }
}