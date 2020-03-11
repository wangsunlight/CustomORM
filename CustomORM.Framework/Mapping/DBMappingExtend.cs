using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CustomORM.Framework.Mapping
{
    public static class DBMappingExtend
    {
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(BaseMappingAttribute), true))
            {
                var attribute = t.GetCustomAttribute<BaseMappingAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return t.Name;
            }
        }

        //老版
        //public static string GetMappingTableName(this Type type)
        //{
        //    if (type.IsDefined(typeof(BaseMappingAttribute), true))
        //    {
        //        var attribute = type.GetCustomAttribute<BaseMappingAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}

        //public static string GetMappingColumnName(this PropertyInfo porp)
        //{
        //    if (porp.IsDefined(typeof(CustomColumnAttribute), true))
        //    {
        //        var attribute = porp.GetCustomAttribute<CustomColumnAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return porp.Name;
        //    }
        //}
    }
}
