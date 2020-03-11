using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomTableAttribute : BaseMappingAttribute
    {
        public CustomTableAttribute(string tableName):base(tableName)
        {
        }
    }
}
