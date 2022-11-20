using DeltaWare.SDK.Core.Validators;
using Microsoft.Data.SqlClient;
using System;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static partial class SqlDbContext
    {
        /// <summary>
        /// Runs the specified SQL Stored Procedure.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="storedProcedure">The Stored Procedure to be executed.</param>
        /// <param name="optionsBuilder">Specifies the options for the Stored Procedure.</param>
        /// <exception cref="MethodAccessException">
        /// Thrown when an invalid database provider is used.
        /// </exception>
        public static ISqlStoredProcedureFactory RunSqlStoredProcedure(this DbContext context, string storedProcedure, Action<IStoredProcedureOptionsBuilder> optionsBuilder = null)
        {
            if (!context.Database.IsSqlServer())
            {
                throw new MethodAccessException($"The database provider must be SQL server provider but was {context.Database.ProviderName}");
            }

            StringValidator.ThrowOnNullOrWhitespace(storedProcedure, nameof(storedProcedure));

            SqlConnection sqlConnection = new SqlConnection(context.Database.GetConnectionString());

            if (optionsBuilder == null)
            {
                return new SqlStoredProcedureFactory(sqlConnection, storedProcedure);
            }

            StoredProcedureOptions options = new();

            optionsBuilder.Invoke(options);

            return new SqlStoredProcedureFactory(sqlConnection, storedProcedure, options);
        }
    }
}