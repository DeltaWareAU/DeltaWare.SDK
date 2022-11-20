using System.Data;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.Parameters
{
    public interface ISqlParameterBuilder
    {
        /// <summary>
        /// Adds the specified parameter to the stored procedure.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="type">The parameter type.</param>
        /// <param name="value">The parameters value.</param>
        void AddParameter(string name, SqlDbType type, object value);

        /// <summary>
        /// Adds the specified parameter to the stored procedure.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="type">The parameter type.</param>
        /// <param name="value">The parameters value.</param>
        void AddParameter(string name, SqlDbType type, int size, object value);

        /// <summary>
        /// Adds the specified parameter to the stored procedure.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="type">The parameter type.</param>
        /// <param name="value">The parameters value.</param>
        void AddParameter(string name, SqlDbType type, int size, string sourceColumn, object value);
    }
}