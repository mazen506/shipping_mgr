using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string Email, string Name, string CallbackUrl, string ActionName);
    }
}
