using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore.Serialization
{
    public interface ISqlDataReaderSerializer
    {
        T Deserialize<T>(SqlDataReader reader);

        object Deserialize(SqlDataReader reader, Type type);

        Task<T> DeserializeAsync<T>(SqlDataReader reader);

        Task<object> DeserializeAsync(SqlDataReader reader, Type type);
    }
}