using DeltaWare.SDK.Serialization.Csv.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public partial class CsvWriter : IDisposable
    {
        private readonly StreamWriter _baseStream;

        private int _expectedRowCount = -1;
        private int _lineNumber;
        private CsvState _state;

        /// <inheritdoc cref="StreamWriter.BaseStream"/>>
        public StreamWriter BaseStream => _baseStream;

        public CsvWriter(StreamWriter baseStream)
        {
            _baseStream = baseStream;
        }

        public void Dispose()
        {
            _baseStream?.Dispose();
        }

        public void Flush()
        {
            _baseStream.Flush();
        }

        public Task FlushAsync()
        {
            return _baseStream.FlushAsync();
        }

        protected virtual string EncapsulateField(string value)
        {
            if (value == null)
            {
                return null;
            }

            bool encapsulate = value.Contains(',') || value.Contains("\r\n");

            if (value.Contains("\""))
            {
                encapsulate = true;

                value = value.Replace("\"", "\"\"");
            }

            if (encapsulate)
            {
                value = $"\"{value}\"";
            }

            return value;
        }
    }
}