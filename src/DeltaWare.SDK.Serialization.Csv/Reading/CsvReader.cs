using DeltaWare.SDK.Serialization.Csv.Enums;
using DeltaWare.SDK.Serialization.Csv.Settings;
using System;
using System.IO;

namespace DeltaWare.SDK.Serialization.Csv.Reading
{
    public partial class CsvReader : IDisposable
    {
        private readonly StreamReader _reader;
        private readonly ICsvReaderSettings _readerSettings;
        private CsvState _state = CsvState.NotWriting;
        public int LineNumber { get; private set; }
        public int LinePosition { get; private set; }

        public CsvReader(StreamReader reader, ICsvReaderSettings settings = null)
        {
            _reader = reader;
            _readerSettings = settings ?? new CsvReaderSettings();
        }

        public CsvReader(StreamReader reader, Action<CsvReaderSettings> settingsBuild)
        {
            _reader = reader;

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

            _reader = new StreamReader(stream);
            _readerSettings = settings ?? new CsvReaderSettings();
        }

        public CsvReader(string value, Action<CsvReaderSettings> settingsBuild)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            _reader = new StreamReader(stream);

            CsvReaderSettings settings = new CsvReaderSettings();

            settingsBuild.Invoke(settings);

            _readerSettings = settings;
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}