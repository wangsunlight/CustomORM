using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework.Validate
{
    public class CustomLengthAttribute : BaseValidateAttribute
    {
        private int _iMin = 0;
        private int _iMax = 0;

        /// <summary>
        /// 左边闭区间，右边开区间
        /// </summary>
        /// <param name="min">包含</param>
        /// <param name="max">不包含</param>
        public CustomLengthAttribute(int min, int max)
        {
            this._iMax = max;
            this._iMin = min;
        }

        public override bool Validate(object oValue)
        {
            return oValue != null
                && !string.IsNullOrWhiteSpace(oValue.ToString())
                && oValue.ToString().Length >= this._iMin
                && oValue.ToString().Length < this._iMax;
        }
    }
}
