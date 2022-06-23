using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Localization;
using ShippingMgr.Core.Resources;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ShippingMgr.Core.Application
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IConfiguration _appSettings;
        private readonly ILocalizeService _localizer;
        private readonly IHelperService _helper;


        public UserService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IEmailService emailService,
                           IJwtAuthManager jwtAuthManager,
                           IConfiguration appSettings,
                           ILocalizeService localizer,
                           IHelperService helper
                           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _appSettings = appSettings;
            _localizer = localizer;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<OperationResult<string>> Register(RegisterInfo model)
        {
            OperationResult<string> result = new OperationResult<string>();
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            //To be changed to dynamic
            //if (model.Locale == "ar")
            //    user.Title_Ar = model.Name;
            //else if (model.Locale == "en")
            //    user.Title_En = model.Name;

            var userResult = await _userManager.CreateAsync(user, model.Password);
            if (userResult.Succeeded)
            {
                result.Success = true;
                try
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, model.Role.ToString());

                    if (roleResult.Succeeded)
                    {
                        result.Data = await SendEmailConfirmationLink(user, "Signup");
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        result.Message = "Invalid Role";
                        result.Success = false;
                    }
                } catch(Exception ex)
                {
                    await _userManager.DeleteAsync(user);
                    result.Message = ex.Message;
                    result.Success = false;
                }
                
            }
            else
                result.Message = userResult.Errors.Select(x => x.Description).FirstOrDefault();
            
            return result;
        }

        public async Task<string> SendEmailConfirmationLink(string email, string actionName = "AccountActivation")
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            return await SendEmailConfirmationLink(user, actionName);
        }

        public async Task<string> SendEmailConfirmationLink(AppUser user, string actionName = "AccountActivation")
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = _appSettings.GetSection("ApplicationURL").GetSection("EmailConfirmationURL").Value;
            var callbackUrl = url + "?email=" + user.Email + "&code=" + HttpUtility.UrlEncode(code);
            //await emailService.SendEmail(user.Email, user.FirstName + ' ' + user.LastName, HtmlEncoder.Default.Encode(callbackUrl), actionName);
            return code;
        }


        public async Task<LoginResult> Login(LoginRequest model)
        {
            var loginResult = new LoginResult();
            var user = await _userManager.FindByNameAsync(model.Email);

            loginResult.Success = false;
            if (user == null)
            {
                loginResult.Message = "InvalidUser";
                return loginResult;
            };

            if (user.EmailConfirmed == false)
            {
                loginResult.Message = "AccountNotActivated";
                return loginResult;
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    loginResult.Message = _localizer.GetValue("YourAccountIsLocked");
                    return loginResult;
                }

                //Get role assigned to the user
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();
                
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, role)
                };

                var jwtAuthResult = _jwtAuthManager.GenerateTokens(user.UserName, claims, DateTime.Now);
                loginResult.Success = true;
                loginResult.Email = user.Email;
                loginResult.FirstName = user.FirstName;
                loginResult.LastName = user.LastName;
                loginResult.Role = role;
                loginResult.AccessToken = jwtAuthResult.AccessToken;
                return loginResult;
            }
            else
            {
                loginResult.Message = "InvalidUserOrPassword";
                return loginResult;
            }
        }

        public async Task<OperationResult<string>> ConfirmEmail(string email, string code)
        {
            OperationResult<string> result = new OperationResult<string>();
            var user = await _userManager.FindByEmailAsync(email);
            IdentityOptions options = new IdentityOptions();
            if (user == null || code == null)
            {
                result.Success = false;
                result.Message = "UnexpectedActivationError";
            }

                var data = await _userManager.ConfirmEmailAsync(user, code);
                if (data.Succeeded)
                {
                    result.Message = "AccountActivatedSuccessfully";
                    result.Success = true;
                }
                else
                {
                    result.Success = false;
                    result.Message = "InvalidActivationLink";
                }
                return result;
        }


        public async Task<OperationResult<UserDetails>> GetUserProfile(string locale,string email)
        {
            OperationResult<UserDetails> result = new OperationResult<UserDetails>();
            var userDetails = new UserDetails();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                result.Success = false;
                result.Message = _localizer.GetValue("AccountNotExists");
                return result;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();
            userDetails.FirstName = user.FirstName;
            userDetails.LastName = user.LastName;
            userDetails.Email = user.Email;
            userDetails.Role = role;
            userDetails.Phone = user.PhoneNumber;
            userDetails.Logo = user.Logo;

            if (locale == "ar")
            {
                userDetails.Title = user.Title_Ar;
                userDetails.Description = user.Description_Ar;
                userDetails.Address = user.Address_Ar;
            }
            else if (locale == "en")
            {
                userDetails.Title = user.Title_En;
                userDetails.Description = user.Description_En;
                userDetails.Address = user.Address_En;
            }
            result.Success = true;
            result.Data = userDetails;
            return result;
        }



        public async Task<OperationResult<string>> SendResetPasswordLink(string email)
        {
            OperationResult<string> result = new OperationResult<string>();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                result.Message = _localizer.GetValue("AccountNotExists");
                result.Success = false;
                return result;
            }
            return await SendResetPasswordLink(user);
        }

        public async Task<OperationResult<string>> SendResetPasswordLink(AppUser user)
        {
            OperationResult<string> result = new OperationResult<string>();

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            result.Data = code;

            var Url = _appSettings.GetSection("ApplicationURL").GetSection("PasswordResetURL").Value;

            var callBackUrl = Url + "?email=" + user.Email + "&code=" + HttpUtility.UrlEncode(code);
            await _emailService.SendEmail(user.Email, user.FirstName, HtmlEncoder.Default.Encode(callBackUrl), "ResetPasswordLink");
            result.Success = true;
            return result;
        }

        public async Task<OperationResult<string>> ResetPassword(ResetPasswordInfo model)
        {
            OperationResult<string> result = new OperationResult<string>();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
               // IdentityOptions options = new IdentityOptions();
                //var proivder = options.Tokens.EmailConfirmationTokenProvider;
                //bool isValid = await userManager.VerifyUserTokenAsync(user, proivder, "ResetPassword", HttpUtility.UrlDecode(model.Code));

                    var resetPassword = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                    if (!resetPassword.Succeeded)
                    {
                        result.Success = true;
                        result.Message = "PasswordUpdatedSuccessfully";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = resetPassword.Errors.ToString();
                    }
            }
            else
            {
                result.Success = false;
                result.Message = "InvalidResetPasswordLink";
            }
            return result;
        }

        public async Task<OperationResult<string>> SaveUserProfile(UserDetails model)
        {
            OperationResult<string> result = new OperationResult<string>();
            var user = await _userManager.FindByIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            user.Logo = model.Logo;
            user.PhoneNumber = model.Phone;
            user.Updated_At = DateTime.UtcNow;
            user.Locale = model.Locale;

            if (model.Locale == "ar")
            {
                user.Title_Ar = model.Title;
                user.Description_Ar = model.Description;
                user.Address_Ar = model.Address;
            }
            else if (model.Locale == "en")
            {
                user.Title_En = model.Title;
                user.Description_En = model.Description;
                user.Address_En = model.Address;
            }
            
            
            var userResult = await _userManager.UpdateAsync(user);

            if (userResult.Succeeded)
            {
                result.Success = true;
                result.Message = _localizer.GetValue("ProfileUpdatedSuccessfully");
            }
            else
            {
                result.Success = false;
                foreach(IdentityError err in userResult.Errors)
                {
                    result.Message += err.Description + Environment.NewLine;
                }
            }
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
   
    }
}
