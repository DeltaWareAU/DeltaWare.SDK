using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Reading
{
    public partial class CsvReader : IDisposable
    {
        public Task<string> ReadAsync()
        {
            return Task.FromResult<string>(Read());
        }

        public async Task<string[]> ReadLineAsync()
        {
            List<string> fields = new List<string>();

            StringBuilder fieldBuilder = new StringBuilder();

            do
            {
                string field = await ReadAsync();

                if (_state.HasFlag(CsvState.Writing))
                {
                    fields.Add(field);
                }
                else
                {
                    fields.Add(null);
                }

                fieldBuilder.Clear();

                LineNumber++;
            }
            while (!_state.HasFlag(CsvState.EndOfLine));

            return fields.ToArray();
        }

        public async Task<string[][]> ReadToEndAsync()
        {
            List<string[]> csvRows = new List<string[]>();

            int expectedRowCount = -1;

            int lineNumber = 1;

            while (!_reader.EndOfStream)
            {
                string[] row = await ReadLineAsync();

                if (expectedRowCount == -1)
                {
                    expectedRowCount = row.Length;
                }
                else if (_readerSettings.MissingColumnHandling == MissingColumnHandling.NotAllow && row.Length != expectedRowCount)
                {
                    throw InvalidCsvDataException.InvalidColumnCount(lineNumber, expectedRowCount, row.Length);
                }

                lineNumber++;

                csvRows.Add(row);
            }

            return csvRows.ToArray();
        }
    }
}