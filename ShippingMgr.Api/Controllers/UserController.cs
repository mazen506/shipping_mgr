using DotNetCore.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Database.Context;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShippingMgr.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : Controller
    {
        private IUserService userService;
        private readonly ILocalizeService resourceLocalizer;

        public UserController(IUserService userService,
                              ILocalizeService resourceLocalizer)
        {
            this.userService = userService;
            this.resourceLocalizer = resourceLocalizer;
        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {

            var result = await userService.Login(model);
            if (result.Success)
                return Ok(result);
            else
                return Unauthorized(new { name = result.Message });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterInfo model)
        {   
            var result = await userService.Register(model);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }

        //Confirm user account
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            OperationResult<string> result = new OperationResult<string>();
            result = await userService.ConfirmEmail(email, code);
            if (result.Success) 
                return Ok();
            else
                return BadRequest(result.Message);
        }


        [HttpGet("SendEmailConfirmationLink")]
        public async Task<IActionResult> SendEmailConfirmationLink(string email)
        {
            var code = await userService.SendEmailConfirmationLink(email);
            if (code != null)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPost("SendResetPasswordLink")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            OperationResult<string> result = new OperationResult<string>();
            result = await userService.SendResetPasswordLink(email);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }


        //Reset user password from email/link
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordInfo model)
        {
            OperationResult<string> result = new OperationResult<string>();
            result = await userService.ResetPassword(model);
            if (result.Success)
                return Ok();
            else
                return BadRequest(result.Message);
        }


        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(string locale, string email)
        {
            var result = await userService.GetUserProfile(locale, email);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("SaveUserProfile")]
        public async Task<IActionResult> SaveUserProfile(UserDetails model)
        {
            var result = await userService.SaveUserProfile(model);
            if (result.Success)
                return Ok(result.Message);
            else
                return BadRequest(result.Message);
        }


        [HttpGet("GetLocalizedInfo")]
        public string GetLocalizedInfo()
        {
            return resourceLocalizer.GetValue("Test");
        }
        //[HttpGet("SendEmail")]
        //public async Task<IActionResult> SendEmail(string message)
        //{
        //    try
        //    {
        //        OperationResult<string> result = new OperationResult<string>();
        //        await emailService.SendEmail("mazen506@gmail.com", "Mazen Mustafa", "http://wesalix.com/work", "Test", "Test Action");
        //        return Ok();
        //    } catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}



        //[HttpPost("addusertorole/{username}/{role}")]
        //public async Task<IActionResult> AddUserToRole(string userName, Role role)
        //{
        //    var user = await userManager.FindByNameAsync(userName);
        //    if (!await userManager.IsInRoleAsync(user, role.ToString()))
        //    {
        //        var result = await userManager.AddToRoleAsync(user, role.ToString());
        //        if (result.Succeeded)
        //            return true.ApiResult();
        //        else
        //            return false.ApiResult();
        //    }
        //    return false.ApiResult();
        //}

        //[HttpGet("listroles")]
        //public IActionResult ListRoles()
        //{
        //    var roles = roleManager.Roles.ToList();
        //    return roles.ApiResult();
        //}

        //[HttpPost("createrole")]
        //public async Task<IActionResult> CreateRole(IdentityRole role)
        //{
        //    var result = await roleManager.CreateAsync(role);
        //    if (result.Succeeded)
        //        return true.ApiResult();
        //    else
        //        return false.ApiResult();
        //}

    }
}
