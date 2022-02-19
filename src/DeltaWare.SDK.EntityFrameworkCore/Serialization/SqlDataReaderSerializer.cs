using DeltaWare.SDK.Core.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeltaWare.SDK.EntityFrameworkCore.Serialization
{
    public class SqlDataReaderSerializer : ISqlDataReaderSerializer
    {
        #region Async

        public async Task<T> DeserializeAsync<T>(SqlDataReader reader)
        {
            return (T)await DeserializeAsync(reader, typeof(T));
        }

        public virtual async Task<object> DeserializeAsync(SqlDataReader reader, Type type)
        {
            PropertyInfo[] columnToPropertyMap;

            if (!type.ImplementsInterface<IList>())
            {
                if (!await reader.ReadAsync())
                {
                    return default;
                }

                columnToPropertyMap = BuildColumnToPropertyMap(reader.GetColumnSchema(), type);

                return DeserializeRowAsync(reader, type, columnToPropertyMap);
            }

            type = type.GetGenericArguments().First();

            columnToPropertyMap = BuildColumnToPropertyMap(reader.GetColumnSchema(), type);

            IList list = GenericTypeHelper.CreateGenericList(type);

            while (await reader.ReadAsync())
            {
                list.Add(await DeserializeRowAsync(reader, type, columnToPropertyMap));
            }

            return list;
        }

        protected virtual async Task<object> DeserializeRowAsync(SqlDataReader reader, Type type, PropertyInfo[] columnToPropertyMap)
        {
            object parentObject = Activator.CreateInstance(type);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (await reader.IsDBNullAsync(i))
                {
                    continue;
                }

                columnToPropertyMap[i]?.SetValue(parentObject, reader.GetValue(i));
            }

            return parentObject;
        }

        #endregion Async

        #region Sync

        public T Deserialize<T>(SqlDataReader reader)
        {
            return (T)Deserialize(reader, typeof(T));
        }

        public virtual object Deserialize(SqlDataReader reader, Type type)
        {
            PropertyInfo[] columnToPropertyMap;

            if (!type.ImplementsInterface<IList>())
            {
                if (!reader.Read())
                {
                    return default;
                }

                columnToPropertyMap = BuildColumnToPropertyMap(reader.GetColumnSchema(), type);

                return DeserializeRow(reader, type, columnToPropertyMap);
            }

            type = type.GetGenericArguments().First();

            columnToPropertyMap = BuildColumnToPropertyMap(reader.GetColumnSchema(), type);

            IList list = GenericTypeHelper.CreateGenericList(type);

            while (reader.Read())
            {
                list.Add(DeserializeRow(reader, type, columnToPropertyMap));
            }

            return list;
        }

        protected virtual object DeserializeRow(SqlDataReader reader, Type type, PropertyInfo[] columnToPropertyMap)
        {
            object parentObject = Activator.CreateInstance(type);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.IsDBNull(i))
                {
                    continue;
                }

                columnToPropertyMap[i]?.SetValue(parentObject, reader.GetValue(i));
            }

            return parentObject;
        }

        #endregion Sync

        protected virtual PropertyInfo[] BuildColumnToPropertyMap(IReadOnlyCollection<DbColumn> columnSchema, Type type)
        {
            PropertyInfo[] properties = type.GetPublicProperties();

            List<PropertyInfo> columnPropertyMap = new();

            foreach (DbColumn column in columnSchema)
            {
                // Match column to property name.
                PropertyInfo property = properties.FirstOrDefault(c => c.Name.Equals(column.ColumnName, StringComparison.InvariantCultureIgnoreCase));

                if (property == null)
                {
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        ColumnAttribute columnAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();

                        if (columnAttribute == null)
                        {
                            continue;
                        }

                        if (!string.IsNullOrWhiteSpace(columnAttribute.Name))
                        {
                            // Match column to attribute name
                            if (columnAttribute.Name.Equals(column.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                property = propertyInfo;

                                break;
                            }
                        }

                        if (columnAttribute.Order >= 0)
                        {
                            // Match column to attribute index.
                            if (columnAttribute.Order == column.ColumnOrdinal)
                            {
                                property = propertyInfo;

                                break;
                            }
                        }
                    }
                }

                columnPropertyMap.Add(property);
            }

            return columnPropertyMap.ToArray();
        }
    }
}