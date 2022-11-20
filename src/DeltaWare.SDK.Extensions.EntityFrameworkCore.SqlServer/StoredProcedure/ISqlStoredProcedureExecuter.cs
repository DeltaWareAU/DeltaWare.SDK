using System.Threading.Tasks;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.StoredProcedure
{
    public interface ISqlStoredProcedureExecuter
    {
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type to be returned by the stored procedure.</typeparam>
        /// <returns>
        /// A new instance of T create from the results of the stored procedure, if nothing was
        /// returned the default is returned.
        /// </returns>
        T Execute<T>();

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <typeparam name="T">The type to be returned by the stored procedure.</typeparam>
        /// <returns>
        /// A new instance of T create from the results of the stored procedure, if nothing was
        /// returned the default is returned.
        /// </returns>
        Task<T> ExecuteAsync<T>();

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set
        /// returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        T ExecuteScalar<T>();

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set
        /// returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        Task<T> ExecuteScalarAsync<T>();
    }
}