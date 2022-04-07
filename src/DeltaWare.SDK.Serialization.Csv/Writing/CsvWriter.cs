using DeltaWare.SDK.Serialization.Csv.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public partial class CsvWriter : IDisposable
    {
        private readonly StreamWriter _writer;

        private int _expectedRowCount = -1;
        private int _lineNumber;
        private CsvState _state;

        public CsvWriter(StreamWriter writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }

        public void Flush()
        {
            _writer.Flush();
        }

        public Task FlushAsync()
        {
            return _writer.FlushAsync();
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