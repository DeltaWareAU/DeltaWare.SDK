using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    public interface ICsvSerializer
    {
        IEnumerable<T> Deserialize<T>(string value, bool hasHeaders = true) where T : class;

        IEnumerable<T> Deserialize<T>(Stream stream, bool hasHeaders = true) where T : class;

        IEnumerable<T> Deserialize<T>(CsvReader reader, bool hasHeaders = true) where T : class;

        Task<IEnumerable<T>> DeserializeAsync<T>(string value, bool hasHeaders = true) where T : class;

        Task<IEnumerable<T>> DeserializeAsync<T>(Stream stream, bool hasHeaders = true) where T : class;

        Task<IEnumerable<T>> DeserializeAsync<T>(CsvReader reader, bool hasHeaders = true) where T : class;

        string Serialize<T>(IEnumerable<T> lines, bool hasHeader = true) where T : class;

        void Serialize<T>(IEnumerable<T> lines, Stream stream, bool hasHeader = true) where T : class;

        void Serialize<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeader = true) where T : class;

        Task<string> SerializeAsync<T>(IEnumerable<T> lines, bool hasHeader = true) where T : class;

        Task SerializeAsync<T>(IEnumerable<T> lines, Stream stream, bool hasHeader = true) where T : class;

        Task SerializeAsync<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeader = true) where T : class;
    }
}