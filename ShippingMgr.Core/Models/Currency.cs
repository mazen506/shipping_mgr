using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Models
{
    public class Currency: BaseEntity
    {
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public string Code_En { get; set; }
        public string Code_Ar { get; set; }
    }
}
