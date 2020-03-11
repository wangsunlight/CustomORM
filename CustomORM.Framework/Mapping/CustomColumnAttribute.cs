using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomColumnAttribute : BaseMappingAttribute
    {
        public CustomColumnAttribute(string columnName) : base(columnName)
        {
        }
    }
}
