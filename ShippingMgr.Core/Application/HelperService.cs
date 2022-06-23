using ShippingMgr.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application
{
    public class HelperService: IHelperService
    {
        public string Locale()
        {
            return CultureInfo.CurrentCulture.Name;
        }
    }
}
