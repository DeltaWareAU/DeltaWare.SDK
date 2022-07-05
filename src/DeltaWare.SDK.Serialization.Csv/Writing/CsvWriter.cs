using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public sealed class CsvWriter : ICsvWriter, IDisposable
    {
        private readonly StreamWriter _baseStream;

        private int _expectedRowCount = -1;
        private int _lineNumber;
        private CsvState _state;

        /// <inheritdoc cref="StreamWriter.BaseStream"/>>
        public StreamWriter BaseStream => _baseStream;

        public CsvType Mode { get; }

        public CsvWriter(StreamWriter baseStream, CsvType mode = CsvType.Default)
        {
            _baseStream = baseStream;

            Mode = mode;
        }

        public void Dispose()
        {
            _baseStream?.Dispose();
        }

        /// <inheritdoc cref="StreamWriter.Flush"/>
        public void Flush()
        {
            _baseStream.Flush();
        }

        /// <inheritdoc cref="StreamWriter.FlushAsync"/>
        public Task FlushAsync()
        {
            return _baseStream.FlushAsync();
        }

        private string EncapsulateField(string value)
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

        #region Async Methods

        public async Task WriteAllAsync(string[][] lines)
        {
            if (_state.HasFlag(CsvState.EndOfFile))
            {
                throw new MethodAccessException("The Stream has reached the end of the file.");
            }

            foreach (string[] line in lines)
            {
                await WriteLineAsync(line);
            }

            _state = CsvState.EndOfFile;
        }

        public async Task WriteAsync(string field, WriteMode type = WriteMode.Field)
        {
            field = EncapsulateField(field);

            switch (type)
            {
                case WriteMode.Field:
                    await _baseStream.WriteAsync($"{field},");
                    break;

                case WriteMode.TerminateLine:
                    await _baseStream.WriteLineAsync($"{field}");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task WriteLineAsync(string[] line)
        {
            _lineNumber++;

            if (Mode == CsvType.Default)
            {
                if (_expectedRowCount == -1)
                {
                    _expectedRowCount = line.Length;
                }
                else if (_expectedRowCount != line.Length)
                {
                    throw InvalidCsvDataException.InvalidColumnCount(_lineNumber, _expectedRowCount, line.Length);
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (i < line.Length - 1)
                {
                    await WriteAsync(line[i]);
                }
                else
                {
                    await WriteAsync(line[i], WriteMode.TerminateLine);
                }
            }
        }

        #endregion

        #region Sync Methods

        public void Write(string field, WriteMode type = WriteMode.Field)
        {
            field = EncapsulateField(field);

            switch (type)
            {
                case WriteMode.Field:
                    _baseStream.Write($"{field},");
                    break;

                case WriteMode.TerminateLine:
                    _baseStream.WriteLine($"{field}");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void WriteAll(string[][] lines)
        {
            if (_state.HasFlag(CsvState.EndOfFile))
            {
                throw new MethodAccessException("The Stream has reached the end of the file.");
            }

            foreach (string[] line in lines)
            {
                WriteLine(line);
            }

            _state = CsvState.EndOfFile;
        }

        public void WriteLine(string[] line)
        {
            _lineNumber++;

            if (Mode == CsvType.Default)
            {
                if (_expectedRowCount == -1)
                {
                    _expectedRowCount = line.Length;
                }
                else if (_expectedRowCount != line.Length)
                {
                    throw InvalidCsvDataException.InvalidColumnCount(_lineNumber, _expectedRowCount, line.Length);
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (i < line.Length - 1)
                {
                    Write(line[i]);
                }
                else
                {
                    Write(line[i], WriteMode.TerminateLine);
                }
            }
        }

        #endregion
    }
}