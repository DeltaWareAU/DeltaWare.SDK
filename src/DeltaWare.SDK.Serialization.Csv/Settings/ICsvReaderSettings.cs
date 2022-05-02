using DeltaWare.SDK.Serialization.Csv.Enums;

namespace DeltaWare.SDK.Serialization.Csv.Settings
{
    public interface ICsvReaderSettings
    {
        MissingColumnHandling MissingColumnHandling { get; }

        int Utf32CarriageReturn { get; }

        int Utf32Comma { get; }

        int Utf32EndOfFile { get; }

        int Utf32LineFeed { get; }

        int Utf32QuotationMark { get; }
    }
}