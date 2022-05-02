using DeltaWare.SDK.Serialization.Csv.Attributes;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System;
using System.Collections.Generic;
using System.Reflection;
using CsvReader = DeltaWare.SDK.Serialization.Csv.Reading.CsvReader;

namespace DeltaWare.SDK.Serialization.Csv
{
    public partial class CsvSerializer : ICsvSerializer
    {
        public IEnumerable<object> Deserialize(CsvReader reader, Type type, bool hasHeaders = true)
        {
            if (!hasHeaders && _attributeCache.TryGetAttribute(type, out HeaderRequiredAttribute _))
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
                        propertyIndex = _headerHandler.BuildHeaderIndex(type, currentLine.Length, currentLine);

                        continue;
                    }

                    propertyIndex = _headerHandler.BuildHeaderIndex(type, currentLine.Length);
                }

                object parentObject = Activator.CreateInstance(type);

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
    }
}