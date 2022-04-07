using DeltaWare.SDK.Serialization.Csv.Enums;

namespace DeltaWare.SDK.Serialization.Csv.Settings
{
    public class CsvReaderSettings : ICsvReaderSettings
    {
        public MissingColumnHandling MissingColumnHandling { get; set; } = MissingColumnHandling.NotAllow;
        public int Utf32CarriageReturn { get; set; } = 13;
        public int Utf32Comma { get; set; } = 44;
        public int Utf32EndOfFile { get; set; } = -1;
        public int Utf32LineFeed { get; set; } = 10;
        public int Utf32QuotationMark { get; set; } = 34;
    }
}