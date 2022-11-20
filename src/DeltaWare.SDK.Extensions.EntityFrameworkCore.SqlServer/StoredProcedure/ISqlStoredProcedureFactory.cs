using System;
using System.Linq.Expressions;
using DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.Parameters;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure
{
    public interface ISqlStoredProcedureFactory : ISqlStoredProcedureExecuter
    {
        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="parameters">The parameters to be executed with the stored procedure.</param>
        ISqlStoredProcedureExecuter WithParameters<TParameters>(TParameters parameters) where TParameters : class;

        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <param name="sqlParameterBuilder">Creates the parameters to be used by the stored procedure.</param>
        ISqlStoredProcedureExecuter WithParameters(Action<ISqlParameterBuilder> sqlParameterBuilder);

        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="parameters">The parameters to be executed with the stored procedure.</param>
        /// <param name="propertySelect">Selects what properties will be included in the parameters.</param>
        ISqlStoredProcedureExecuter WithParameters<TParameters>(TParameters parameters, Expression<Func<TParameters, object>> propertySelect) where TParameters : class;

        /// <summary>
        /// Execute the stored procedure with the specified parameters.
        /// </summary>
        /// <typeparam name="TParameters">The type of the parameter.</typeparam>
        /// <param name="builder">Builds the parameter object to be used by the stored procedure.</param>
        ISqlStoredProcedureExecuter WithParameters<TParameters>(Action<TParameters> builder) where TParameters : class;
    }
}