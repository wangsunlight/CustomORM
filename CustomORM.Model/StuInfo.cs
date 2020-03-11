using CustomORM.Framework.Mapping;
using CustomORM.Framework.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Model
{
    [CustomTable("stuInfo")]
    public class StuInfo : BaseModel
    {
        [CustomRequired]
        [CustomColumn("stuName")]
        public string StuName { get; set; }

    }
}
