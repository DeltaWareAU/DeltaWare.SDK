using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.SqlServer.Serialization
{
    public interface ISqlDataReaderSerializer
    {
        T Deserialize<T>(SqlDataReader reader);

        object Deserialize(SqlDataReader reader, Type type);

        Task<T> DeserializeAsync<T>(SqlDataReader reader);

        Task<object> DeserializeAsync(SqlDataReader reader, Type type);
    }
}