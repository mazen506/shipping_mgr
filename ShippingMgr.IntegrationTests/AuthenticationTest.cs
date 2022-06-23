using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Database.Context;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace ShippingMgr.IntegrationTests
{
    public class AuthenticationTest: IClassFixture<TestBase<Program>>
    {
        private HttpClient _client;
        private TestBase<Program> _factory;
        private readonly IServiceProvider _serviceProvider;
        public AuthenticationTest(TestBase<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
            _serviceProvider = factory.GetServiceProvider();
        }

        [Fact]
        public async Task LoginShouldReturnValidJwtWhenSuccessfull()
        {
            //var _client = _factory.CreateClient();
            var credentials = new LoginRequest()
            {
                Email = "mazen506@gmail.com",
                Password = "Test@1234"
            };

            string json = JsonSerializer.Serialize(credentials);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var loginResponseContent = await response.Content.ReadAsStringAsync();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var loginResult = JsonSerializer.Deserialize<LoginResult>(loginResponseContent, jsonOptions);
            Assert.Equal(credentials.Email, loginResult.Email);
            Assert.Equal(UserRoles.Admin, loginResult.Role);
            Assert.False(string.IsNullOrWhiteSpace(loginResult.AccessToken));

            //Decode Token
            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            var (principal, jwtSecurityToken) = jwtAuthManager.DecodeJwtToken(loginResult.AccessToken);
            Assert.Equal(credentials.Email, principal.Identity.Name);
            Assert.Equal(UserRoles.Admin, principal.FindFirst(ClaimTypes.Role).Value);
            Assert.NotNull(jwtSecurityToken);
        }

        [Theory]
        [InlineData("mazen506@hotmail.com", UserRoles.Client, HttpStatusCode.Forbidden)]
        [InlineData("mazen506@gmail.com", UserRoles.Admin, HttpStatusCode.Created)]
        public async Task ShouldAllowCreateCurrencyForAdmin(string email, string role, HttpStatusCode expectedStatusCode)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };
            var jwtAuthManager = _serviceProvider.GetRequiredService<IJwtAuthManager>();
            var jwtResult = jwtAuthManager.GenerateTokens(email, claims, DateTime.Now.AddMinutes(-1));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtResult.AccessToken);

            var request = new CurrencyVM { Name_Ar="فرنك جيبوتي", Name_En="Frank", Code_Ar="فرنك", Code_En="FDJ" };

            var response = await _client.PostAsync("api/currencies/create",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json));

            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task Login_should_return_error_with_invalid_credentials()
        {
            var content = new Dictionary<string, string>()
            {
                { "Email", "mazen506@gmail.com" },
                { "Password", "Test@123" }
            };
            string json = JsonSerializer.Serialize(content);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", httpContent);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("mazen506@hotmail.com","Admin", HttpStatusCode.OK)]
        [InlineData("mazen506@hotmail.com", "admin1", HttpStatusCode.BadRequest)]
        [InlineData("mazen506@gmail.com", "Admin", HttpStatusCode.BadRequest)]
        public async Task Signup_should_return_success_with_valid_info(string email, string role, HttpStatusCode expectedResult)
        {
            var content = new Dictionary<string, string>()
            {
                { "Email", email },
                { "Password", "Test@1234" },
                { "Role", role },
                { "FirstName", "Mazen" },
                { "LastName","Mustafa" },
                { "Locale", "en" }
            };
            string json = JsonSerializer.Serialize(content);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            //Check register
            var response = await _client.PostAsync("/api/auth/register", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedResult, response.StatusCode);

            //Check Confirm
            if (response.StatusCode == HttpStatusCode.OK)
            {
                httpContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");
                response = await _client.PostAsync("/api/auth/ConfirmEmail?email=mazen506@hotmail.com&code=" + HttpUtility.UrlEncode(responseContent), httpContent);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Theory]
        [InlineData("mazen506@gmail.com", HttpStatusCode.OK)]
        public async Task ResetPassword_should_return_success_with_valid_info(string email, HttpStatusCode expectedResult)
        {
            StringContent httpContent = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            //Check Sending Operation
            var response = await _client.PostAsync("/api/auth/SendResetPasswordLink?email=" + email, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(expectedResult, response.StatusCode);

            //Check Confirm
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = new Dictionary<string, string>()
            {
                { "Email", email },
                { "Password", "Admin@1234" },
                { "Code", HttpUtility.UrlEncode(responseContent) }
            };
                string json = JsonSerializer.Serialize(content);

                httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await _client.PostAsync("/api/auth/ResetPassword", httpContent);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }



    }
}