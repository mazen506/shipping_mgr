using Microsoft.Extensions.Localization;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application
{
    public class LocalizeService: ILocalizeService
    {
        private readonly IStringLocalizer<GlobalResource> resourceLocalizer;
        public LocalizeService(IStringLocalizer<GlobalResource> resourceLocalizer)
        {
            this.resourceLocalizer = resourceLocalizer;
        }


        public string GetValue(string key)
        {
            return resourceLocalizer.GetString("Test").Value;
        }
    }
}
