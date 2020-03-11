using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CustomORM.Framework;
using CustomORM.Framework.ExtendExpression.Visitor;
using CustomORM.Framework.Mapping;
using CustomORM.Framework.Validate;
using CustomORM.Model;

namespace CustomORM.DAL
{
    public class SqlHelper
    {
        public T Find<T>(int id) where T : BaseModel, new()
        {
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.FindOne);
            SqlParameter[] sqlParameterList = new SqlParameter[] {
            new SqlParameter("@Id",id)
            };

            return this.ExecuteSql(sql, sqlParameterList, comm =>
            {
                var reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    //T t = Activator.CreateInstance<T>();
                    T t = new T();
                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);
                    }
                    return t;
                }
                else
                {
                    return default;
                }
            }, SqlConnectionStringPool.DBOperateType.Read);
        }

        public bool Insert<T>(T t) where T : BaseModel, new()
        {
            if (!t.ValidateModel())
            {
                return false;
            }
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.FindOne);
            SqlParameter[] sqlParameterList = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            return this.ExecuteSql(sql, sqlParameterList, comm =>
               comm.ExecuteNonQuery() == 1);
        }

        public bool Update<T>(T t) where T : BaseModel, new()
        {
            if (!t.ValidateModel())
            {
                return false;
            }
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.Update);
            SqlParameter[] sqlParameterList = type.GetProperties().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).Append(new SqlParameter("@Id", t.Id)).ToArray();

            return this.ExecuteSql(sql, sqlParameterList, comm =>
               comm.ExecuteNonQuery() == 1);
        }

        public bool Delete<T>(int id) where T : BaseModel, new()
        {
            Type type = typeof(T);
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderType.Delete);
            SqlParameter[] sqlParameterList = new SqlParameter[] {
                new SqlParameter("@Id", id)
            };
            return this.ExecuteSql(sql, sqlParameterList, comm => comm.ExecuteNonQuery() == 1);
        }

        private S ExecuteSql<S>(string sql, SqlParameter[] parameters, Func<SqlCommand, S> func, SqlConnectionStringPool.DBOperateType dBOperateType = SqlConnectionStringPool.DBOperateType.Write)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionStringPool.GetConnectionString(dBOperateType)))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                return func.Invoke(command);
            }
        }

        public bool Delete<T>(Expression<Func<T, bool>> expression)
        {
            Type type = typeof(T);
            CustomExpressionVisitor visitor = new CustomExpressionVisitor();
            visitor.Visit(expression);
            string where = visitor.GetWhere();
            string sql = $"DELETE FROM {type.GetMappingName()} WHERE {where}";
            return this.ExecuteSql(sql, new SqlParameter[0], comm => comm.ExecuteNonQuery() >= 1);
        }

        public IEnumerable<T> Select<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            Type type = typeof(T);
            CustomExpressionVisitor visitor = new CustomExpressionVisitor();
            visitor.Visit(expression);
            string where = visitor.GetWhere();
            string sql = $"SELECT * FROM {type.GetMappingName()} WHERE {where}";

            return this.ExecuteSql(sql, new SqlParameter[0], comm =>
            {
                List<T> list = new List<T>();
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    T t = new T();
                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);
                    }
                    list.Add(t);
                }
                return list;
            }, SqlConnectionStringPool.DBOperateType.Read);
        }
    }
}
