using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DeltaWare.SDK.Core.Helpers;
using DeltaWare.SDK.Core.Validators;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.Helpers;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.Parameters;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.Serialization;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure.Options;
using Microsoft.Data.SqlClient;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure
{
    internal class SqlStoredProcedureFactory : ISqlStoredProcedureFactory
    {
        private static readonly ISqlDataReaderSerializer Serializer = new SqlDataReaderSerializer();

        private readonly SqlCommand _command;
        private readonly SqlConnection _connection;

        public SqlStoredProcedureFactory(SqlConnection connection, string storedProcedure)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));

            StringValidator.ThrowOnNullOrWhitespace(storedProcedure, nameof(storedProcedure));

            try
            {
                _command = new SqlCommand(storedProcedure, _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
            }
            catch
            {
                _connection?.Dispose();

                throw;
            }
        }

        public SqlStoredProcedureFactory(SqlConnection connection, string storedProcedure, IStoredProcedureOptions options) : this(connection, storedProcedure)
        {
            if (options.Timeout.HasValue)
            {
                _command.CommandTimeout = options.Timeout.Value;
            }
        }

        public T Execute<T>()
        {
            try
            {
                _connection.Open();

                using SqlDataReader reader = _command.ExecuteReader();

                return Serializer.Deserialize<T>(reader);
            }
            finally
            {
                _command.Dispose();
                _connection.Dispose();
            }
        }

        public async Task<T> ExecuteAsync<T>()
        {
            try
            {
                await _connection.OpenAsync();

                await using SqlDataReader reader = await _command.ExecuteReaderAsync();

                return await Serializer.DeserializeAsync<T>(reader);
            }
            finally
            {
                await _command.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }

        public T ExecuteScalar<T>()
        {
            try
            {
                _connection.Open();

                return _command.ExecuteScalar<T>();
            }
            finally
            {
                _command.Dispose();
                _connection.Dispose();
            }
        }

        public async Task<T> ExecuteScalarAsync<T>()
        {
            try
            {
                await _connection.OpenAsync();

                return await _command.ExecuteScalarAsync<T>();
            }
            finally
            {
                await _command.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }

        public ISqlStoredProcedureExecuter WithParameters<TParameters>(TParameters parameters) where TParameters : class
        {
            try
            {
                IEnumerable<PropertyInfo> properties = typeof(TParameters).GetPublicProperties();

                CompileParameters(properties, parameters);

                return this;
            }
            catch
            {
                _command.Dispose();
                _connection.Dispose();

                throw;
            }
        }

        public ISqlStoredProcedureExecuter WithParameters(Action<ISqlParameterBuilder> sqlParameterBuilder)
        {
            try
            {
                SqlParameterBuilder builder = new SqlParameterBuilder(_command);

                sqlParameterBuilder.Invoke(builder);

                return this;
            }
            catch
            {
                _command.Dispose();
                _connection.Dispose();

                throw;
            }
        }

        public ISqlStoredProcedureExecuter WithParameters<TParameters>(TParameters parameters, Expression<Func<TParameters, object>> propertySelect) where TParameters : class
        {
            try
            {
                IEnumerable<PropertyInfo> properties = PropertySelectorHelper.GetSelectedProperties(propertySelect);

                CompileParameters(properties, parameters);

                return this;
            }
            catch
            {
                _command.Dispose();
                _connection.Dispose();

                throw;
            }
        }

        public ISqlStoredProcedureExecuter WithParameters<TParameters>(Action<TParameters> builder) where TParameters : class
        {
            try
            {
                TParameters parameters = Activator.CreateInstance<TParameters>();

                builder.Invoke(parameters);

                IEnumerable<PropertyInfo> properties = typeof(TParameters).GetPublicProperties();

                CompileParameters(properties, parameters);

                return this;
            }
            catch
            {
                _command.Dispose();
                _connection.Dispose();

                throw;
            }
        }

        #region Execute Methods

        private void CompileParameter(string name, SqlDbType type, object value)
        {
            _command.Parameters.Add($"@{name}", type).Value = value;
        }

        private void CompileParameters<T>(IEnumerable<PropertyInfo> properties, T parentObject)
        {
            foreach (PropertyInfo property in properties)
            {
                CompileParameter(property.Name, SqlHelper.GetSqlDbType(property.PropertyType), property.GetValue(parentObject));
            }
        }

        #endregion Execute Methods
    }
}