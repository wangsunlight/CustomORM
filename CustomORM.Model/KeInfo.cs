using CustomORM.Framework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Model
{
    [CustomTable("keInfo")]
    public class KeInfo : BaseModel
    {
        public string keName { get; set; }
    }
}
