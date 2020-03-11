using CustomORM.Framework.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomORM.Model
{
    [CustomTable("scInfo")]
    public class ScInfo : BaseModel
    {
        public int stuid { get; set; }

        public int keid { get; set; }

        public double score { get; set; }
    }
}
