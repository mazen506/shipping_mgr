using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.ViewModels
{
    public class CurrencyVM: BaseVM
    {
        public string? Name_En { get; set; }
        public string? Name_Ar { get; set; }
        public string? Code_En { get; set; }
        public string? Code_Ar { get; set; }
    }
}
