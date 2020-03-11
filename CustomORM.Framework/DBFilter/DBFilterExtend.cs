using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Framework
{
    public static class DBFilterExtend
    {
        /// <summary>
        /// 获取不包含Key的所有字段
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesWithNotKey(this Type type)
        {
            return type.GetProperties().Where(p => !p.IsDefined(typeof(CustomKeyAttribute), true));
        }
    }
}
