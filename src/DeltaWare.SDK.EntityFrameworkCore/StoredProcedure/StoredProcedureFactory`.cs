using DeltaWare.SDK.Core.Validators;
using DeltaWare.SDK.Serialization.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DeltaWare.SDK.EntityFrameworkCore.StoredProcedure
{
    internal class StoredProcedureFactory<TEntity> : IStoredProcedureFactory<TEntity> where TEntity : class
    {
        private const string QueryParameterFormatter = "@{0} = '{1}' ";

        private readonly DbSet<TEntity> _entities;
        private readonly IObjectSerializer _objectSerializer = new ObjectSerializer();
        private readonly string _storedProcedure;
        private Dictionary<string, string> _parameters = new();

        public StoredProcedureFactory(DbSet<TEntity> entities, string storedProcedure)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            StringValidator.ThrowOnNullOrWhitespace(storedProcedure, nameof(storedProcedure));

            _entities = entities;

            _storedProcedure = storedProcedure;
        }

        public IQueryable<TEntity> Execute()
        {
            return _entities.FromSqlRaw(BuildRawSqlQuery());
        }

        public IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(TParameters parameters) where TParameters : class
        {
            _parameters = _objectSerializer.SerializeToDictionary(parameters);

            return this;
        }

        public IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(TParameters parameters, Expression<Func<TParameters, object>> propertySelect) where TParameters : class
        {
            _parameters = _objectSerializer.SerializeToDictionary(parameters, propertySelect);

            return this;
        }

        public IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(Action<TParameters> builder) where TParameters : class
        {
            TParameters parameters = Activator.CreateInstance<TParameters>();

            builder.Invoke(parameters);

            _parameters = _objectSerializer.SerializeToDictionary(parameters);

            return this;
        }

        private string BuildParameter(string key, string value)
        {
            return string.Format(QueryParameterFormatter, key, value);
        }

        private string BuildRawSqlQuery()
        {
            StringBuilder queryBuilder = new();

            queryBuilder.Append(_storedProcedure);

            foreach ((string key, string value) in _parameters)
            {
                queryBuilder.Append(BuildParameter(key, value));
                queryBuilder.Append(',');
            }

            return queryBuilder.ToString().TrimEnd(',');
        }
    }
}