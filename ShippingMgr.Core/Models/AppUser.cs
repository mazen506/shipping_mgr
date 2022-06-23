using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ShippingMgr.Core.Models
{
    public class AppUser: IdentityUser
    {
        public DateTime Email_Verified_At { get; set; }
        public string? Remember_Token { get; set; }
        public int Currency_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title_En { get; set; }
        public string? Title_Ar { get; set; }
        public string? Description_En { get; set; }
        public string? Description_Ar { get; set; }
        public string? Address_En { get; set; }
        public string? Address_Ar { get; set; }
        public string? Logo { get; set; }
        public string? Locale { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
    }

    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class RegisterInfo
    {
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string Locale { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ResetPasswordInfo
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }


    public class UserDetails
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Locale { get; set; }
        public string Role { get; set; }

    }

    public class ChangePasswordInfo
    {
        public string UserID { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }


    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Vendor = nameof(Vendor);
        public const string Client = nameof(Client);
    }

    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        public AppUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options
            ) : base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Email",
                user.Email
                ));
            return identity;
        }
    }
}
