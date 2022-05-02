using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Settings;
using System;
using System.IO;
using System.Text;

namespace DeltaWare.SDK.Serialization.Csv.Reading
{
    public partial class CsvReader : IDisposable
    {
        private readonly StreamReader _baseStream;
        private readonly ICsvReaderSettings _readerSettings;
        private CsvState _state = CsvState.NotWriting;

        public int LineNumber { get; private set; }

        public int LinePosition { get; private set; }

        /// <inheritdoc cref="StreamReader.EndOfStream"/>>
        public bool EndOfStream => _baseStream.EndOfStream;

        /// <inheritdoc cref="StreamReader.BaseStream"/>>
        public StreamReader BaseStream => _baseStream;

        /// <inheritdoc cref="StreamReader.BaseStream"/>>
        public Encoding CurrentEncoding => _baseStream.CurrentEncoding;

        public CsvReader(StreamReader baseStream, ICsvReaderSettings settings = null)
        {
            _baseStream = baseStream;
            _readerSettings = settings ?? new CsvReaderSettings();
        }

        public CsvReader(StreamReader baseStream, Action<CsvReaderSettings> settingsBuild)
        {
            _baseStream = baseStream;

            CsvReaderSettings settings = new CsvReaderSettings();

            settingsBuild.Invoke(settings);

            _readerSettings = settings;
        }

        public CsvReader(string value, ICsvReaderSettings settings = null)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            _baseStream = new StreamReader(stream);
            _readerSettings = settings ?? new CsvReaderSettings();
        }

        public CsvReader(string value, Action<CsvReaderSettings> settingsBuild)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            _baseStream = new StreamReader(stream);

            CsvReaderSettings settings = new CsvReaderSettings();

            settingsBuild.Invoke(settings);

            _readerSettings = settings;
        }

        public void Dispose()
        {
            _baseStream?.Dispose();
        }
    }
}