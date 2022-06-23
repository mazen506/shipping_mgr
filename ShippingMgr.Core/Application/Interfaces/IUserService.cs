using Microsoft.AspNetCore.Identity;
using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingMgr.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult<string>> Register(RegisterInfo model);
        Task<LoginResult> Login(LoginRequest model);
        Task<OperationResult<UserDetails>> GetUserProfile(string locale, string email);
        Task<OperationResult<string>> SaveUserProfile(UserDetails model);
        Task LogOut();
        Task<string> SendEmailConfirmationLink(string email, string actionName = "AccountActivation");
        Task<string> SendEmailConfirmationLink(AppUser user, string actionName = "AccountActivation");
        Task<OperationResult<string>> ConfirmEmail(string email, string code);
        Task<OperationResult<string>> SendResetPasswordLink(string email);
        Task<OperationResult<string>> SendResetPasswordLink(AppUser user);
        Task<OperationResult<string>> ResetPassword(ResetPasswordInfo model);
        
    }
}
