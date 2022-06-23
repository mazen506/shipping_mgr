using Microsoft.AspNetCore.Mvc;
using Moq;
using ShippingMgr.Controllers;
using ShippingMgr.Core.Application;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShippingMgr.Tests
{
    public class UserControllerTest:TestBase
    {
        private UserService _userService;
        private UserController _controller;
        public UserControllerTest()
        {
        }

        [Fact]
        public async Task Login_should_return_success()
        {
        }

        public async Task Register_should_return_success()
        {
        }
    }
}
