using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Validate
{
    public class CustomRequiredAttribute : BaseValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue == null && string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}
