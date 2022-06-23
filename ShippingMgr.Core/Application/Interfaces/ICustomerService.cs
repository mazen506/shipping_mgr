using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.Results;
using DotNetCore.Validation;
using ShippingMgr.Core.ViewModels;

namespace ShippingMgr.Core.Application.Interfaces
{
    public interface ICustomerService: IEntityService<Customer, CustomerVM>
    {
        
    }
}
