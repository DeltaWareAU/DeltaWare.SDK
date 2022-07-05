using DeltaWare.SDK.Serialization.Csv.Enums;
using System.IO;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv.Writing
{
    public interface ICsvWriter
    {
        /// <inheritdoc cref="StreamWriter.BaseStream"/>>
        StreamWriter BaseStream { get; }

        CsvType Mode { get; }

        void Dispose();

        /// <inheritdoc cref="StreamWriter.Flush"/>
        void Flush();

        /// <inheritdoc cref="StreamWriter.FlushAsync"/>
        Task FlushAsync();

        Task WriteAllAsync(string[][] lines);

        Task WriteAsync(string field, WriteMode type = WriteMode.Field);

        Task WriteLineAsync(string[] line);

        void Write(string field, WriteMode type = WriteMode.Field);

        void WriteAll(string[][] lines);

        void WriteLine(string[] line);
    }
}
