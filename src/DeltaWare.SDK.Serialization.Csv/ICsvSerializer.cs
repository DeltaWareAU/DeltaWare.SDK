using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    public interface ICsvSerializer
    {
        IEnumerable<object> Deserialize(CsvReader reader, Type type, bool hasHeaders = true);

        Task<IEnumerable<object>> DeserializeAsync(CsvReader reader, Type type, bool hasHeaders = true);

        void Serialize<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class;

        Task SerializeAsync<T>(IEnumerable<T> lines, CsvWriter writer, bool hasHeaders = true) where T : class;
    }

    public static class CsvSerializerExtensions
    {
        public static IEnumerable<T> Deserialize<T>(this ICsvSerializer serializer, string value, bool hasHeaders = true) where T : class
        {
            using CsvReader csvReader = new CsvReader(value);

            return serializer.Deserialize(csvReader, typeof(T), hasHeaders).Cast<T>();
        }

        public static IEnumerable<T> Deserialize<T>(this ICsvSerializer serializer, Stream stream, bool hasHeaders = true) where T : class
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            return serializer.Deserialize(csvReader, typeof(T), hasHeaders).Cast<T>();
        }

        public static IEnumerable<T> Deserialize<T>(this ICsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where T : class
        {
            return serializer.Deserialize(reader, typeof(T), hasHeaders).Cast<T>();
        }

        public static Task<IEnumerable<T>> DeserializeAsync<T>(this ICsvSerializer serializer, string value, bool hasHeaders = true) where T : class
        {
            using CsvReader csvReader = new(value);

            return serializer.DeserializeAsync(csvReader, typeof(T), hasHeaders).CastAsync<T>();
        }

        public static Task<IEnumerable<T>> DeserializeAsync<T>(this ICsvSerializer serializer, Stream stream, bool hasHeaders = true) where T : class
        {
            StreamReader reader = new(stream);

            CsvReader csvReader = new(reader);

            return serializer.DeserializeAsync(csvReader, typeof(T), hasHeaders).CastAsync<T>();
        }

        public static Task<IEnumerable<T>> DeserializeAsync<T>(this ICsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where T : class
        {
            return serializer.DeserializeAsync(reader, typeof(T), hasHeaders).CastAsync<T>();
        }

        public static string Serialize<T>(this ICsvSerializer serializer, IEnumerable<T> lines, bool hasHeader = true) where T : class
        {
            using Stream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            serializer.Serialize(lines, csvWriter, hasHeader);

            stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static void Serialize<T>(this ICsvSerializer serializer, IEnumerable<T> lines, Stream stream, bool hasHeader = true) where T : class
        {
            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            serializer.Serialize(lines, csvWriter, hasHeader);
        }

        public static async Task<string> SerializeAsync<T>(this ICsvSerializer serializer, IEnumerable<T> lines, bool hasHeader = true) where T : class
        {
            await using Stream stream = new MemoryStream();

            StreamWriter writer = new(stream);

            CsvWriter csvWriter = new(writer);

            await serializer.SerializeAsync(lines, csvWriter, hasHeader);

            stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new(stream);

            return await reader.ReadToEndAsync();
        }

        public static Task SerializeAsync<T>(this ICsvSerializer serializer, IEnumerable<T> lines, Stream stream, bool hasHeader = true) where T : class
        {
            StreamWriter writer = new(stream);

            CsvWriter csvWriter = new(writer);

            return serializer.SerializeAsync(lines, csvWriter, hasHeader);
        }
    }
}