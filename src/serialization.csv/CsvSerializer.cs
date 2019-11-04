

namespace DeltaWare.Tools.Serialization.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Attributes;
    using Exceptions;

    /// <summary>
    /// A set of tools for working with Csv Files.
    /// </summary>
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
        #region Public Fields
        /// <summary>
        /// Gets or sets a <see cref="bool"/> value specifying if the first row of a Csv should be ignored.
        /// </summary>
        public bool IgnoreFirstRow { get; set; }
        #endregion Public Fields
        #region Public Methods
        /// <summary>
        /// Deserializes a Csv formatted <see cref="string"/> into the specified <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="object"/> type to deserialize too.</typeparam>
        /// <param name="content">The Csv formatted <see cref="string"/> to deserialize from.</param>
        /// <returns>The specified <see cref="object"/> containing the deserialized Csv data.</returns>
        public T[] Deserialize<T>(string content)
        {
            bool carriageReturnHit = false;
            bool doubleQuoteHit = false;

            char[] contentArray = content.ToCharArray();

            Dictionary<int, int> columnIndexOverrides = new Dictionary<int, int>();

            FieldInfo[] fieldInfoArray = typeof(T).GetFields();

            int columnIndex = 0;
            int rowIndex = 0;
            int csvWidth = 0;

            int fileLine = -1;
            int fileRow = 0;

            List<T> csvBuilder = new List<T>();

            string itemBuilder = string.Empty;

            if (IgnoreFirstRow)
            {
                rowIndex = -1;
            }
            
            for (int index = 0; index < contentArray.Length; index++)
            {
                fileLine++;

                char currentChar = contentArray[index];

                if (currentChar == ColumnSeparator && !doubleQuoteHit)
                {
                    NextColumn(itemBuilder, fieldInfoArray, csvBuilder, columnIndexOverrides, columnIndex, rowIndex);

                    columnIndex++;
                    itemBuilder = string.Empty;
                }
                else if (currentChar == CarriageReturn && !doubleQuoteHit)
                {
                    if (csvWidth == 0)
                    {
                        csvWidth = columnIndex;
                    }

                    if (columnIndex != csvWidth || carriageReturnHit)
                    {
                        throw new CsvFormatException($"An Unexpected Carriage Return was hit at Row: {fileRow} Line: {fileLine}.");
                    }

                    carriageReturnHit = true;
                }
                else if (carriageReturnHit && !doubleQuoteHit)
                {
                    if (currentChar == NewLine)
                    {
                        if (columnIndex != csvWidth)
                        {
                            throw new CsvFormatException($"An Unexpected New Line was hit at Row: {fileRow} Line: {fileLine}.");
                        }

                        NextColumn(itemBuilder, fieldInfoArray, csvBuilder, columnIndexOverrides, columnIndex, rowIndex);

                        columnIndex = 0;
                        rowIndex++;

                        fileLine = -1;
                        fileRow++;

                        carriageReturnHit = false;
                    }
                    else
                    {
                        throw new CsvFormatException($"An Error was found at Row: {fileRow} Line: {fileLine}");
                    }

                    itemBuilder = string.Empty;
                }
                else if (currentChar == DoubleQuote)
                {
                    if (doubleQuoteHit)
                    {
                        if (contentArray.Length == index + 1 || contentArray[index + 1] == ColumnSeparator || contentArray[index + 1] == CarriageReturn)
                        {
                            doubleQuoteHit = false;
                        }
                        else
                        {
                            itemBuilder += currentChar;
                        }
                    }
                    else
                    {
                        doubleQuoteHit = true;
                    }
                }
                else
                {
                    itemBuilder += currentChar;
                }
            }

            return csvBuilder.ToArray();
        }

        public string SerialToString<T>(IEnumerable<T> content)
        {
            return SerialToString(content.ToArray());
        }

        public string SerialToString<T>(T[] content)
        {
            throw new NotImplementedException();

            bool carriageReturnHit = false;
            bool doubleQuoteHit = false;

            //Dictionary<int, int> columnIndexOverrides = new Dictionary<int, int>();

            FieldInfo[] fieldInfoArray = typeof(T).GetFields();

            int columnIndex = 0;
            int rowIndex = 0;
            int csvWidth = fieldInfoArray.Length;
            
            int fileLine = -1;
            int fileRow = 0;

            List<T> csvBuilder = new List<T>();

            string itemBuilder = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            if (IgnoreFirstRow)
            {
                foreach (FieldInfo fieldInfo in fieldInfoArray)
                {
                    stringBuilder.Append(fieldInfo.Attributes);
                }
            }
        }
        #endregion Public Methods
        #region Private Methods
        /// <summary>
        /// Finds the <see cref="FieldInfo"/> index name associated with the content value else finds the <see cref="FieldInfo"/> index associated with the column index.
        /// </summary>
        /// <param name="fieldinfoarray">An array of <see cref="FieldInfo"/>.</param>
        /// <param name="content">The <see cref="string"/> value that is compared with the <see cref="FieldInfo"/> name.</param>
        /// <returns>An <see cref="int"/> value specifying the index found.</returns>
        private int FindColumnOverride(FieldInfo[] fieldinfoarray, string content)
        {
            return Array.FindIndex(
                fieldinfoarray,
            fieldInfo =>
                {
                    if (Attribute.IsDefined(fieldInfo, typeof(ColumnName)))
                    {
                        if (((ColumnName)fieldInfo.GetCustomAttributes(typeof(ColumnName), false).First()).Name.Equals(content))
                        {
                            if (!IgnoreFirstRow)
                            {
                                throw new ArgumentException("Cannot use First Row of CSV in Conjunction with Column Names.");
                            }
                        
                            return true;
                        }
                    }
                    else if (Attribute.IsDefined(fieldInfo, typeof(ColumnIndex)))
                    {
                        if (((ColumnIndex)fieldInfo.GetCustomAttributes(typeof(ColumnIndex), false).First()).Id.Equals(
                            Convert.ToInt32(content)))
                        {
                            return true;
                        }
                    }

                    return false;
                });
        }

        private void NextColumn<T>(string itembuilder, FieldInfo[] fieldinfoarray, List<T> csvbuilder, Dictionary<int, int> columnindexoverrides, int columnindex, int rowindex)
        {
            if (rowindex == -1)
            {
                int columnIndexOverride = FindColumnOverride(fieldinfoarray, itembuilder);

                if (columnIndexOverride == -1)
                {
                    return;
                }

                if (columnindexoverrides.ContainsValue(columnIndexOverride))
                {
                    throw new DuplicateColumnException();
                }

                columnindexoverrides.Add(columnindex, columnIndexOverride);
            }
            else
            {
                if (columnindex == 0)
                {
                    csvbuilder.Add((T)Activator.CreateInstance(typeof(T)));
                }

                try
                {
                    if (columnindexoverrides.ContainsKey(columnindex))
                    {
                        fieldinfoarray[columnindexoverrides[columnindex]].SetValue(
                            csvbuilder[rowindex],
                            Convert.ChangeType(itembuilder,fieldinfoarray[columnindexoverrides[columnindex]].FieldType));
                    }
                }
                catch (InvalidCastException invalidCastException)
                {
                    throw new InvalidCastException(
                        $"Could not Convert CSV Field [{rowindex}:{columnindexoverrides[columnindex]}] to Specified Type " +
                        fieldinfoarray[columnindexoverrides[columnindex]].FieldType,
                        invalidCastException);
                }
            }
        }
        #endregion Private Methods
    }
}
