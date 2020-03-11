using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Mapping
{
    public class BaseMappingAttribute : Attribute
    {
        private string _MappingName = null;
        public BaseMappingAttribute(string mappingName)
        {
            this._MappingName = mappingName;
        }

        public string GetMappingName()
        {
            return this._MappingName;
        }
    }
}
