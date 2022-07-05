﻿using DeltaWare.SDK.Serialization.Csv.Exceptions;
using DeltaWare.SDK.Serialization.Csv.Reading;
using DeltaWare.SDK.Serialization.Csv.Writing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Serialization.Csv
{
    /// <summary>
    /// Deserializes and Serializes CSV data.
    /// </summary>
    public interface ICsvSerializer
    {
        Task<object> DeserializeRecordsAsync(CsvReader reader, Type containerSchema);
        Task DeserializeRecordsAsync(CsvReader reader, object container);

        Task<IEnumerable<object>> DeserializeRecordsAsync(CsvReader reader, params Type[] schema);

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="schema">The <see cref="Type"/> for the data to be serialize to.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        IEnumerable<object> Deserialize(CsvReader reader, Type schema, bool hasHeaders = true);

        /// <summary>
        /// Deserializes the CSV data into the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="reader">The reader to be deserialized.</param>
        /// <param name="schema">The <see cref="Type"/> for the data to be serialize to.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <returns>Returns the deserialized data or an empty <see cref="IEnumerable{T}"/> if no data was present.</returns>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        /// <exception cref="InvalidCsvDataException">Thrown when the Csv Data is invalid.</exception>
        Task<IEnumerable<object>> DeserializeAsync(CsvReader reader, Type schema, bool hasHeaders = true);

        /// <summary>
        /// Serializes the CSV data.
        /// </summary>
        /// <typeparam name="TSchema">The type being serialized.</typeparam>
        /// <param name="lines">The data to be serialized.</param>
        /// <param name="writer">The writer for the serialized data to be written to.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        void Serialize<TSchema>(IEnumerable<TSchema> lines, CsvWriter writer, bool hasHeaders = true) where TSchema : class;

        /// <summary>
        /// Serializes the CSV data.
        /// </summary>
        /// <typeparam name="TSchema">The type being serialized.</typeparam>
        /// <param name="lines">The data to be serialized.</param>
        /// <param name="writer">The writer for the serialized data to be written to.</param>
        /// <param name="hasHeaders">Specifies if the CSV contains headers.</param>
        /// <exception cref="CsvSchemaException">Thrown when the Csv Schema is invalid.</exception>
        Task SerializeAsync<TSchema>(IEnumerable<TSchema> lines, CsvWriter writer, bool hasHeaders = true) where TSchema : class;
    }
}