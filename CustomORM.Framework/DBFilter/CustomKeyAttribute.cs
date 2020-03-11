using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Framework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomKeyAttribute : Attribute
    {
    }
}
