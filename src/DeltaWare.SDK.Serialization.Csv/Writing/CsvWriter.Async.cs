using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public partial class CsvWriter : IDisposable
    {
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

        public async Task WriteAsync(string field, FieldType type = FieldType.Field)
        {
            field = EncapsulateField(field);

            switch (type)
            {
                case FieldType.Field:
                    await _baseStream.WriteAsync($"{field},");
                    break;

                case FieldType.EndField:
                    await _baseStream.WriteLineAsync($"{field}");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public async Task WriteLineAsync(string[] line)
        {
            _lineNumber++;

            if (_expectedRowCount == -1)
            {
                _expectedRowCount = line.Length;
            }
            else if (_expectedRowCount != line.Length)
            {
                throw InvalidCsvDataException.InvalidColumnCount(_lineNumber, _expectedRowCount, line.Length);
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (i < line.Length - 1)
                {
                    await WriteAsync(line[i]);
                }
                else
                {
                    await WriteAsync(line[i], FieldType.EndField);
                }
            }
        }
    }
}