using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application.Interfaces
{
    public interface ILocalizeService
    {
        public  string GetValue(string key);
    }
}
