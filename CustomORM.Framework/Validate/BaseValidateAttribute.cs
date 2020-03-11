using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Validate
{
    /// <summary>
    /// 效验基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class BaseValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }
}
