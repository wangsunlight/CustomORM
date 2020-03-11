using CustomORM.Framework;
using CustomORM.Framework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomORM.Model;

namespace CustomORM.DAL
{
    /// <summary>
    /// 负责生成sql，缓存重用的
    /// </summary>
    public class SqlCacheBuilder<T> where T : BaseModel, new()
    {
        private static string _InsertSql = null;
        private static string _FindSql = null;
        private static string _UpdateSql = null;
        private static string _DeleteSql = null;
        static SqlCacheBuilder()
        {
            Type type = typeof(T);
            {
                string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
                _FindSql = $@"SELECT {columnString} FROM [{type.GetMappingName()}] WHERE Id=@Id";
            }
            {
                string columnString = string.Join(",", type.GetPropertiesWithNotKey().Select(p => $"[{p.GetMappingName()}]"));
                string valueString = string.Join(",", type.GetPropertiesWithNotKey().Select(p => $"@{p.GetMappingName()}"));
                _InsertSql = $@"INSERT INTO [{type.GetMappingName()}] ({columnString}) VALUES ({valueString})";
            }
            {
                string columnValueString = string.Join(",", type.GetPropertiesWithNotKey().Select(p => $"{p.GetMappingName()}=@{p.GetMappingName()}"));

                _UpdateSql = $@"UPDATE [{type.GetMappingName()}] SET {columnValueString} WHERE Id=@Id";
            }
            {
                _DeleteSql = $"DELETE FROM [{type.GetMappingName()}] where Id =@Id";
            }
        }

        public static string GetSql(SqlCacheBuilderType sqlCacheBuilderType)
        {
            switch (sqlCacheBuilderType)
            {
                case SqlCacheBuilderType.FindOne:
                    return _FindSql;
                case SqlCacheBuilderType.Insert:
                    return _InsertSql;
                case SqlCacheBuilderType.Update:
                    return _UpdateSql;
                case SqlCacheBuilderType.Delete:
                    return _DeleteSql;
                default:
                    throw new Exception("Unknown SqlCacheBuilderType");
            }
        }
    }
    public enum SqlCacheBuilderType
    {
        FindOne,
        Insert,
        Update,
        Delete
    }
}
