using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaWare.SDK.Serialization.Csv.Reading
{
    public partial class CsvReader : IDisposable
    {
        public string Read()
        {
            _state = CsvState.NotWriting;

            StringBuilder fieldBuilder = new();

            do
            {
                int utf32 = _reader.Read();

                LinePosition++;

                // End Of File.
                if (utf32 == _readerSettings.Utf32EndOfFile)
                {
                    if (_state.HasFlag(CsvState.EncapsulateField))
                    {
                        throw InvalidCsvDataException.EncapsulationFieldTerminationExpectedEndOfFile(LineNumber, LinePosition);
                    }

                    _state = CsvState.EndOfField | CsvState.EndOfLine | CsvState.EndOfFile;
                }
                else if (utf32 == _readerSettings.Utf32Comma)
                {
                    if (_state.HasFlag(CsvState.EncapsulateField))
                    {
                        // We've hit a comma whilst in an encapsulated field, write that data.
                        _state |= CsvState.Writing;
                    }
                    else
                    {
                        // We've hit a comma so we're at the end of the field.
                        _state = CsvState.EndOfField;
                    }
                }
                else if (utf32 == _readerSettings.Utf32CarriageReturn)
                {
                    if (_state.HasFlag(CsvState.EncapsulateField))
                    {
                        // We've hit a carriage return whilst in an encapsulated field, write that data.
                        _state |= CsvState.Writing;
                    }
                    else if (_reader.Peek() == _readerSettings.Utf32LineFeed)
                    {
                        _reader.Read();

                        // We've hit a new line
                        _state = CsvState.EndOfField | CsvState.EndOfLine;
                    }
                    else
                    {
                        throw InvalidCsvDataException.ExpectedLineFeed(LineNumber, LinePosition);
                    }
                }
                else if (_state.HasFlag(CsvState.FieldTerminated))
                {
                    // An encapsulated field was terminated and we did not terminate the field,
                    // throw an exception.
                    throw InvalidCsvDataException.EncapsulationFieldTerminationExpected(LineNumber, LinePosition);
                }
                else if (utf32 == _readerSettings.Utf32QuotationMark)
                {
                    if (_state.HasFlag(CsvState.EncapsulateField))
                    {
                        if (_reader.Peek() == _readerSettings.Utf32QuotationMark)
                        {
                            utf32 = _reader.Read();

                            LinePosition++;

                            _state |= CsvState.Writing;
                        }
                        else
                        {
                            _state = CsvState.FieldTerminated;
                        }
                    }
                    else if (fieldBuilder.Length == 0)
                    {
                        _state |= CsvState.EncapsulateField;
                    }
                    else
                    {
                        throw InvalidCsvDataException.IllegalCharacterInNonEncapsulatedField(LineNumber, LinePosition);
                    }
                }
                else
                {
                    _state |= CsvState.Writing;
                }

                if (_state.HasFlag(CsvState.Writing))
                {
                    fieldBuilder.Append(char.ConvertFromUtf32(utf32));
                }
            }
            while (!_state.HasFlag(CsvState.EndOfField));

            if (fieldBuilder.Length > 0)
            {
                _state |= CsvState.Writing;
            }

            return fieldBuilder.ToString();
        }

        public string[] ReadLine()
        {
            List<string> fields = new();

            StringBuilder fieldBuilder = new();

            do
            {
                string field = Read();

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

        public string[][] ReadToEnd()
        {
            List<string[]> csvRows = new();

            int expectedRowCount = -1;

            int lineNumber = 1;

            while (!_reader.EndOfStream)
            {
                string[] row = ReadLine();

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