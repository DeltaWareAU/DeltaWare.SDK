using System;
using System.Collections.Generic;
using System.Data;

namespace DeltaWare.SDK.Extensions.EntityFrameworkCore.Helpers
{
    public static class SqlHelper
    {
        private static readonly Dictionary<Type, SqlDbType> TypeMap;

        static SqlHelper()
        {
            TypeMap = new Dictionary<Type, SqlDbType>
            {
                [typeof(string)] = SqlDbType.NVarChar,
                [typeof(char[])] = SqlDbType.NVarChar,
                [typeof(byte)] = SqlDbType.TinyInt,
                [typeof(short)] = SqlDbType.SmallInt,
                [typeof(int)] = SqlDbType.Int,
                [typeof(long)] = SqlDbType.BigInt,
                [typeof(byte[])] = SqlDbType.Image,
                [typeof(bool)] = SqlDbType.Bit,
                [typeof(DateTime)] = SqlDbType.DateTime2,
                [typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset,
                [typeof(decimal)] = SqlDbType.Money,
                [typeof(float)] = SqlDbType.Real,
                [typeof(double)] = SqlDbType.Float,
                [typeof(TimeSpan)] = SqlDbType.Time,
                [typeof(Guid)] = SqlDbType.UniqueIdentifier
            };
        }

        public static SqlDbType GetSqlDbType(Type giveType)
        {
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (TypeMap.ContainsKey(giveType))
            {
                return TypeMap[giveType];
            }

            throw new ArgumentException($"{giveType.Name} is not a supported type, if the type you would like to use is not supported please use the ParameterBuilder instead.");
        }

        public static SqlDbType GetSqlDbType<T>()
        {
            return GetSqlDbType(typeof(T));
        }
    }
}