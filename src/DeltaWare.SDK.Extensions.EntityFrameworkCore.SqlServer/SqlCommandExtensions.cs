using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.SqlClient
{
    public static class SqlCommandExtensions
    {
        #region Scalar

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set
        /// returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        public static T ExecuteScalar<T>(this SqlCommand command)
        {
            object result = command.ExecuteScalar();

            if (result is null or DBNull)
            {
                return default;
            }

            return (T)result;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set
        /// returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set, or a null reference.</returns>
        public static async Task<T> ExecuteScalarAsync<T>(this SqlCommand command)
        {
            object result = await command.ExecuteScalarAsync();

            if (result is null or DBNull)
            {
                return default;
            }

            return (T)result;
        }

        #endregion Scalar
    }
}