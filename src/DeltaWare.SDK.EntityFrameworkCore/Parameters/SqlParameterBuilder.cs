using Microsoft.Data.SqlClient;
using System.Data;

namespace DeltaWare.SDK.EntityFrameworkCore.Parameters
{
    internal class SqlParameterBuilder : ISqlParameterBuilder
    {
        private readonly SqlCommand _command;

        public SqlParameterBuilder(SqlCommand command)
        {
            _command = command;
        }

        public void AddParameter(string name, SqlDbType type, object value)
        {
            _command.Parameters.Add(name, type).Value = value;
        }

        public void AddParameter(string name, SqlDbType type, int size, object value)
        {
            _command.Parameters.Add(name, type, size).Value = value;
        }

        public void AddParameter(string name, SqlDbType type, int size, string sourceColumn, object value)
        {
            _command.Parameters.Add(name, type, size, sourceColumn).Value = value;
        }
    }
}