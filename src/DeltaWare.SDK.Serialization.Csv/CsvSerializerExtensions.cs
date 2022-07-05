using DeltaWare.SDK.Serialization.Csv.Exceptions;
using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    public static class CsvSerializerExtensions
    {
        #region Records

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, string value) where TContainerSchema : class
        {
            using CsvReader csvReader = new CsvReader(value);

            return serializer.DeserializeRecordsAsync(csvReader, typeof(TContainerSchema)).CastAsync<TContainerSchema>();
        }

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, string value) where TContainerSchema : class
        {
            return DeserializeRecordsAsync<TContainerSchema>((ICsvSerializer)serializer, value);
        }

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, Stream stream) where TContainerSchema : class
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            return serializer.DeserializeRecordsAsync(csvReader, typeof(TContainerSchema)).CastAsync<TContainerSchema>();
        }

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, Stream stream) where TContainerSchema : class
        {
            return DeserializeRecordsAsync<TContainerSchema>((ICsvSerializer)serializer, stream);
        }

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, CsvReader reader) where TContainerSchema : class
        {
            return serializer.DeserializeRecordsAsync(reader, typeof(TContainerSchema)).CastAsync<TContainerSchema>();
        }

        public static Task<TContainerSchema> DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, CsvReader reader) where TContainerSchema : class
        {
            return DeserializeRecordsAsync<TContainerSchema>((ICsvSerializer)serializer, reader);
        }

        // -------------

        public static Task DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, string value, TContainerSchema container) where TContainerSchema : class
        {
            using CsvReader csvReader = new CsvReader(value);

            return serializer.DeserializeRecordsAsync(csvReader, container);
        }

        public static Task DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, string value, TContainerSchema container) where TContainerSchema : class
        {
            return DeserializeRecordsAsync<TContainerSchema>((ICsvSerializer)serializer, value);
        }

        public static Task DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, Stream stream, TContainerSchema container) where TContainerSchema : class
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            return serializer.DeserializeRecordsAsync(csvReader, container);
        }

        public static Task DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, Stream stream, TContainerSchema container) where TContainerSchema : class
        {
            return DeserializeRecordsAsync((ICsvSerializer)serializer, stream, container);
        }

        public static Task DeserializeRecordsAsync<TContainerSchema>(this ICsvSerializer serializer, CsvReader reader, TContainerSchema container) where TContainerSchema : class
        {
            return serializer.DeserializeRecordsAsync(reader, container);
        }

        public static Task DeserializeRecordsAsync<TContainerSchema>(this CsvSerializer serializer, CsvReader reader, TContainerSchema container) where TContainerSchema : class
        {
            return DeserializeRecordsAsync((ICsvSerializer)serializer, reader, container);
        }

        // -------------

        public static Task<IEnumerable<object>> DeserializeRecordsAsync(this ICsvSerializer serializer, string value, params Type[] recordTypes)
        {
            using CsvReader csvReader = new CsvReader(value);

            return serializer.DeserializeRecordsAsync(csvReader, recordTypes);
        }

        public static Task<IEnumerable<object>> DeserializeRecordsAsync(this CsvSerializer serializer, string value, params Type[] recordTypes)
        {
            return DeserializeRecordsAsync((ICsvSerializer)serializer, value, recordTypes);
        }

        public static Task<IEnumerable<object>> DeserializeRecordsAsync(this ICsvSerializer serializer, Stream stream, params Type[] recordTypes)
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            return serializer.DeserializeRecordsAsync(csvReader, recordTypes);
        }

        public static Task<IEnumerable<object>> DeserializeRecordsAsync(this CsvSerializer serializer, Stream stream, params Type[] recordTypes)
        {
            return DeserializeRecordsAsync((ICsvSerializer)serializer, stream, recordTypes);
        }

        #endregion



        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="value">The data to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this ICsvSerializer serializer, string value, bool hasHeaders = true) where TSchema : class
        {
            using CsvReader csvReader = new CsvReader(value);

            return serializer.Deserialize(csvReader, typeof(TSchema), hasHeaders).Cast<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="value">The data to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this CsvSerializer serializer, string value, bool hasHeaders = true) where TSchema : class
        {
            return Deserialize<TSchema>((ICsvSerializer)serializer, value, hasHeaders);
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="value">The data to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this ICsvSerializer serializer, string value, bool hasHeaders = true) where TSchema : class
        {
            using CsvReader csvReader = new(value);

            return serializer.DeserializeAsync(csvReader, typeof(TSchema), hasHeaders).CastAsync<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="value">The data to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this CsvSerializer serializer, string value, bool hasHeaders = true) where TSchema : class
        {
            return DeserializeAsync<TSchema>((ICsvSerializer)serializer, value, hasHeaders);
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="stream">The stream to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this ICsvSerializer serializer, Stream stream, bool hasHeaders = true) where TSchema : class
        {
            StreamReader reader = new StreamReader(stream);

            CsvReader csvReader = new CsvReader(reader);

            return serializer.Deserialize(csvReader, typeof(TSchema), hasHeaders).Cast<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="stream">The stream to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this CsvSerializer serializer, Stream stream, bool hasHeaders = true) where TSchema : class
        {
            return Deserialize<TSchema>((ICsvSerializer)serializer, stream, hasHeaders);
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="stream">The stream to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this ICsvSerializer serializer, Stream stream, bool hasHeaders = true) where TSchema : class
        {
            StreamReader reader = new(stream);

            CsvReader csvReader = new(reader);

            return serializer.DeserializeAsync(csvReader, typeof(TSchema), hasHeaders).CastAsync<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="stream">The stream to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this CsvSerializer serializer, Stream stream, bool hasHeaders = true) where TSchema : class
        {
            return DeserializeAsync<TSchema>((ICsvSerializer)serializer, stream, hasHeaders);
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this ICsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where TSchema : class
        {
            return serializer.Deserialize(reader, typeof(TSchema), hasHeaders).Cast<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static IEnumerable<TSchema> Deserialize<TSchema>(this CsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where TSchema : class
        {
            return Deserialize<TSchema>((ICsvSerializer)serializer, reader, hasHeaders);
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this ICsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where TSchema : class
        {
            return serializer.DeserializeAsync(reader, typeof(TSchema), hasHeaders).CastAsync<TSchema>();
        }

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TSchema">The <see cref="Type"/> for the data to be deserialized to.</typeparam>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        public static Task<IEnumerable<TSchema>> DeserializeAsync<TSchema>(this CsvSerializer serializer, CsvReader reader, bool hasHeaders = true) where TSchema : class
        {
            return DeserializeAsync<TSchema>((ICsvSerializer)serializer, reader, hasHeaders);
        }

        public static string Serialize<TSchema>(this ICsvSerializer serializer, IEnumerable<TSchema> lines, bool hasHeader = true) where TSchema : class
        {
            using Stream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            serializer.Serialize(lines, csvWriter, hasHeader);

            stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public static string Serialize<TSchema>(this CsvSerializer serializer, IEnumerable<TSchema> lines, bool hasHeader = true) where TSchema : class
        {
            return Serialize((ICsvSerializer)serializer, lines, hasHeader);
        }

        public static void Serialize<TSchema>(this ICsvSerializer serializer, IEnumerable<TSchema> lines, Stream stream, bool hasHeader = true) where TSchema : class
        {
            StreamWriter writer = new StreamWriter(stream);

            CsvWriter csvWriter = new CsvWriter(writer);

            serializer.Serialize(lines, csvWriter, hasHeader);
        }

        public static void Serialize<TSchema>(this CsvSerializer serializer, IEnumerable<TSchema> lines, Stream stream, bool hasHeader = true) where TSchema : class
        {
            Serialize((ICsvSerializer)serializer, lines, stream, hasHeader);
        }

        public static async Task<string> SerializeAsync<TSchema>(this ICsvSerializer serializer, IEnumerable<TSchema> lines, bool hasHeader = true) where TSchema : class
        {
            await using Stream stream = new MemoryStream();

            StreamWriter writer = new(stream);

            CsvWriter csvWriter = new(writer);

            await serializer.SerializeAsync(lines, csvWriter, hasHeader);

            stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new(stream);

            return await reader.ReadToEndAsync();
        }

        public static Task<string> SerializeAsync<TSchema>(this CsvSerializer serializer, IEnumerable<TSchema> lines, bool hasHeader = true) where TSchema : class
        {
            return SerializeAsync((ICsvSerializer)serializer, lines, hasHeader);
        }

        public static Task SerializeAsync<TSchema>(this ICsvSerializer serializer, IEnumerable<TSchema> lines, Stream stream, bool hasHeader = true) where TSchema : class
        {
            StreamWriter writer = new(stream);

            CsvWriter csvWriter = new(writer);

            return serializer.SerializeAsync(lines, csvWriter, hasHeader);
        }

        public static Task SerializeAsync<TSchema>(this CsvSerializer serializer, IEnumerable<TSchema> lines, Stream stream, bool hasHeader = true) where TSchema : class
        {
            return SerializeAsync((ICsvSerializer)serializer, lines, stream, hasHeader);
        }
    }
}
