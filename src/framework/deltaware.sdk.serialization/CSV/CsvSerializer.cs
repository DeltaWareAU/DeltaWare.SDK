
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using DeltaWare.SDK.Serialization.CSV.Attributes;

namespace DeltaWare.SDK.Serialization.CSV
{
    public class CsvSerializer
    {
        #region Constant Values

        /// <summary>
        /// The <see cref="char"/> used to separate columns.
        /// </summary>
        private const char ColumnSeparator = ',';

        /// <summary>
        /// The <see cref="char"/> used to represent double quotes.
        /// </summary>
        private const char DoubleQuote = '"';

        /// <summary>
        /// The <see cref="char"/> used to represent carriage return.
        /// </summary>
        private const char CarriageReturn = '\r';

        /// <summary>
        /// The <see cref="char"/> used to represent new line.
        /// </summary>
        private const char NewLine = '\n';

        #endregion Constant Values

        #region Public Methods
        /// <summary>
        /// Deserializes a Csv formatted <see cref="string"/> into the specified <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="object"/> type to deserialize too.</typeparam>
        /// <param name="content">The string value to deserialize</param>
        /// <param name="containsHeaders">The CSV has headers.</param>
        /// <returns>The specified <see cref="object"/> containing the deserialized Csv data.</returns>
        public T[] Deserialize<T>(string content, bool containsHeaders = true)
        {
            bool carriageReturnHit = false;
            bool doubleQuoteHit = false;

            Dictionary<int, int> columnIndexOverrides = new Dictionary<int, int>();

            FieldInfo[] objectFields = typeof(T).GetFields();

            int columnIndex = 0;
            int rowIndex = 0;
            int csvWidth = 0;

            int fileLine = 0;
            int fileRow = 0;

            List<T> objects = new List<T>();

            string objectBuilder = string.Empty;

            if (containsHeaders)
            {
                rowIndex = -1;
            }
            
            for (int index = 0; index < content.Length; index++)
            {
                char currentChar = content[index];

                if (currentChar == ColumnSeparator && !doubleQuoteHit)
                {
                    NextColumn(objectBuilder, objectFields, objects, columnIndexOverrides, columnIndex, rowIndex, containsHeaders);

                    columnIndex++;
                    objectBuilder = string.Empty;
                }
                else if (currentChar == CarriageReturn && !doubleQuoteHit)
                {
                    if (csvWidth == 0)
                    {
                        csvWidth = columnIndex;
                    }

                    if (columnIndex != csvWidth || carriageReturnHit)
                    {
                        throw new FormatException($"An Unexpected Carriage Return was hit at Row: {fileRow} Line: {fileLine}.");
                    }

                    carriageReturnHit = true;
                }
                else if (carriageReturnHit && !doubleQuoteHit)
                {
                    if (currentChar == NewLine)
                    {
                        if (columnIndex != csvWidth)
                        {
                            throw new FormatException($"An Unexpected New Line was hit at Row: {fileRow} Line: {fileLine}.");
                        }

                        NextColumn(objectBuilder, objectFields, objects, columnIndexOverrides, columnIndex, rowIndex, containsHeaders);

                        columnIndex = 0;
                        rowIndex++;

                        fileLine = 0;
                        fileRow++;

                        carriageReturnHit = false;
                    }
                    else
                    {
                        throw new FormatException($"An Error was found at Row: {fileRow} Line: {fileLine}");
                    }

                    objectBuilder = string.Empty;
                }
                else if (currentChar == DoubleQuote)
                {
                    if (doubleQuoteHit)
                    {
                        if (content.Length == index + 1 || content[index + 1] == ColumnSeparator || content[index + 1] == CarriageReturn)
                        {
                            doubleQuoteHit = false;
                        }
                        else
                        {
                            objectBuilder += currentChar;
                        }
                    }
                    else
                    {
                        doubleQuoteHit = true;
                    }
                }
                else
                {
                    objectBuilder += currentChar;
                }

                fileLine++;
            }

            return objects.ToArray();
        }

        public string Serialize<T>(T[] values, bool includeHeaders = true)
        {
            bool carriageReturnHit = false;
            bool doubleQuoteHit = false;

            FieldInfo[] fieldInfoArray = typeof(T).GetFields();

            int columnIndex = 0;
            int rowIndex = 0;
            int csvWidth = fieldInfoArray.Length;
            
            int fileLine = -1;
            int fileRow = 0;

            List<T> csvBuilder = new List<T>();

            string itemBuilder = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            if (includeHeaders)
            {
                for (int i = 0; i < fieldInfoArray.Length - 1; i++)
                {
                    stringBuilder.Append(fieldInfoArray[i].Name);
                    stringBuilder.Append(ColumnSeparator);
                }

                stringBuilder.Append(fieldInfoArray.Last().Name);
                stringBuilder.Append($"{CarriageReturn}{NewLine}");
            }

            foreach (T value in values)
            {
                for (int i = 0; i < fieldInfoArray.Length - 1; i++)
                {
                    stringBuilder.Append(GetStringValue(fieldInfoArray[i], value));
                }

                stringBuilder.Append(GetStringValue(fieldInfoArray.Last(), value, true));
            }

            return stringBuilder.ToString();
        }
        public void Serialize<T>(TextWriter textWriter, T[] values, bool includeHeaders = true)
        {
            FieldInfo[] fieldInfoArray = typeof(T).GetFields();

            int csvWidth = fieldInfoArray.Length;
            
            List<T> csvBuilder = new List<T>();

            string itemBuilder = string.Empty;

            if (includeHeaders)
            {
                for (int i = 0; i < fieldInfoArray.Length - 1; i++)
                {
                    textWriter.Write(fieldInfoArray[i].Name);
                    textWriter.Write(ColumnSeparator);
                }

                textWriter.Write(fieldInfoArray.Last().Name);
                textWriter.Write($"{CarriageReturn}{NewLine}");
            }

            foreach (T value in values)
            {
                for (int i = 0; i < fieldInfoArray.Length - 1; i++)
                {
                    textWriter.Write(GetStringValue(fieldInfoArray[i], value));
                }

                textWriter.Write(GetStringValue(fieldInfoArray.Last(), value, true));
            }
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Finds the <see cref="FieldInfo"/> index name associated with the values value else finds the <see cref="FieldInfo"/> index associated with the column index.
        /// </summary>
        /// <param name="objectFields">An array of <see cref="FieldInfo"/>.</param>
        /// <param name="content">The <see cref="string"/> value that is compared with the <see cref="FieldInfo"/> name.</param>
        /// <returns>An <see cref="int"/> value specifying the index found.</returns>
        private static int FindColumnOverride(FieldInfo[] objectFields, string content, bool containsHeaders)
        {
            return Array.FindIndex(
                objectFields,
            fieldInfo =>
                {
                    if (Attribute.IsDefined(fieldInfo, typeof(ColumnNameOverrideAttribute)))
                    {
                        if (!((ColumnNameOverrideAttribute) fieldInfo.GetCustomAttributes(typeof(ColumnNameOverrideAttribute), false).First()).Name.Equals(content))
                        {
                            return false;
                        }

                        if (!containsHeaders)
                        {
                            throw new ArgumentException("Cannot use First Row of CSV in Conjunction with Column Names.");
                        }
                        
                        return true;
                    }
                    else if (Attribute.IsDefined(fieldInfo, typeof(ColumnIndexOverrideAttribute)))
                    {
                        if (((ColumnIndexOverrideAttribute)fieldInfo.GetCustomAttributes(typeof(ColumnIndexOverrideAttribute), false).First()).Id.Equals(Convert.ToInt32(content)))
                        {
                            return true;
                        }
                    }

                    return false;
                });
        }

        private static void NextColumn<T>(string objectBuilder, FieldInfo[] objectFields, List<T> objects, Dictionary<int, int> columnIndexOverrides, int columnIndex, int rowIndex, bool containsHeaders)
        {
            if (rowIndex == -1)
            {
                int columnIndexOverride = FindColumnOverride(objectFields, objectBuilder, containsHeaders);

                if (columnIndexOverride == -1)
                {
                    return;
                }

                if (columnIndexOverrides.ContainsValue(columnIndexOverride))
                {
                    throw new DuplicateNameException("A duplicate column was found.");
                }

                columnIndexOverrides.Add(columnIndex, columnIndexOverride);
            }
            else
            {
                if (columnIndex == 0)
                {
                    objects.Add((T)Activator.CreateInstance(typeof(T)));
                }

                try
                {
                    if (columnIndexOverrides.ContainsKey(columnIndex))
                    {
                        objectFields[columnIndexOverrides[columnIndex]].SetValue(
                            objects[rowIndex],
                            Convert.ChangeType(objectBuilder,objectFields[columnIndexOverrides[columnIndex]].FieldType));
                    }
                    else
                    {
                        objectFields[columnIndex].SetValue
                        (
                            objects[rowIndex],
                            Convert.ChangeType
                            (
                                objectBuilder,
                                objectFields[columnIndex].FieldType
                            )
                        );
                    }
                }
                catch (InvalidCastException invalidCastException)
                {
                    throw new InvalidCastException(
                        $"Could not Convert CSV Field [{rowIndex}:{columnIndexOverrides[columnIndex]}] to Specified Type " +
                        objectFields[columnIndexOverrides[columnIndex]].FieldType,
                        invalidCastException);
                }
            }
        }

        private string GetStringValue(FieldInfo fieldInfo, object value, bool endOfRow = false)
        {
            string stringValue = fieldInfo.GetValue(value)?.ToString() ?? string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            if (stringValue.Contains(ColumnSeparator))
            {
                stringBuilder.Append($"\"{stringValue}\"");
            }
            else
            {
                stringBuilder.Append(stringValue);
            }

            if (endOfRow)
            {
                stringBuilder.Append($"{CarriageReturn}{NewLine}");
            }
            else
            {
                stringBuilder.Append(ColumnSeparator);
            }

            return stringBuilder.ToString();
        }
        #endregion Private Methods
    }
}
