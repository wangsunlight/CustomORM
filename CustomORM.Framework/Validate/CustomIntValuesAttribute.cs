using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CustomORM.Framework.Validate
{
    public class CustomIntValuesAttribute : BaseValidateAttribute
    {
        private int[] _Values = null;

        /// <summary>
        /// 指定包含
        /// </summary>
        /// <param name="values"></param>
        public CustomIntValuesAttribute(params int[] values)
        {
            this._Values = values;
        }

        public override bool Validate(object oValue)
        {
            return oValue != null
                && !string.IsNullOrWhiteSpace(oValue.ToString())
                && int.TryParse(oValue.ToString(), out int iValue)
                && this._Values != null
                && this._Values.Contains(iValue);
        }
    }
}
