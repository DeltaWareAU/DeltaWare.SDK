using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using System;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public partial class CsvWriter : IDisposable
    {
        public void Write(string field, FieldType type = FieldType.Field)
        {
            field = EncapsulateField(field);

            switch (type)
            {
                case FieldType.Field:
                    _baseStream.Write($"{field},");
                    break;

                case FieldType.EndField:
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
                    Write(line[i]);
                }
                else
                {
                    Write(line[i], FieldType.EndField);
                }
            }
        }
    }
}