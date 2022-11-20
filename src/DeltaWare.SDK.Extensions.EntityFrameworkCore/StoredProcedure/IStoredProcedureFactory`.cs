using System;
using System.Linq.Expressions;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.StoredProcedure
{
    public interface IStoredProcedureFactory<out TEntity> : IStoredProcedureExecuter<TEntity> where TEntity : class
    {
        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="parameters">The parameters to be executed with the stored procedure.</param>
        IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(TParameters parameters) where TParameters : class;

        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="parameters">The parameters to be executed with the stored procedure.</param>
        /// <param name="propertySelect">Selects what properties will be included in the parameters.</param>
        IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(TParameters parameters, Expression<Func<TParameters, object>> propertySelect) where TParameters : class;

        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="builder">Builds the parameter object to be used by the stored procedure.</param>
        IStoredProcedureExecuter<TEntity> WithParameters<TParameters>(Action<TParameters> builder) where TParameters : class;
    }
}