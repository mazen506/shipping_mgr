using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DotNetCore.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ShippingMgr.Controllers;
using ShippingMgr.Core.Application;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Database.Context;
using ShippingMgr.Core.Mapping;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShippingMgr.Tests
{
    public class CustomerControllerTest: IClassFixture<TestBase>
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerController _controller;

        public CustomerControllerTest(TestBase testBase)
        {
            //_service = GetCustomerService();
            //_mapper = GetMapper();
            _customerService = new CustomerService(testBase.GetDbContext(), testBase.GetMapper());
            _controller = new CustomerController(_customerService);
        }
            
        [Fact]
        public async Task List_should_return_success_with_collection()
        {
            var result = await _controller.List();

            Assert.IsType<OkObjectResult>(result);

            var objectResult = (OkObjectResult)result;

            Assert.IsType<List<CustomerVM>>(objectResult.Value);
        }

        [Fact]
        public async Task Create_should_return_success_with_valid_model()
        {
            var model = new CustomerVM()
            {
                Name = "Saeed Ghaleb",
                Address = "Djibouti",
                Phone = "0025355444"
            };

            var result = await _controller.Create(model);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Create_should_return_error_with_invalid_model()
        {
            var model = new CustomerVM()
            {
                Name = "Saddam",
                Address = "Djibouti"
            };
            var result = await _controller.Create(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_should_return_success()
        {

            var resultList = await _controller.List();
            var resultListObject = (OkObjectResult)resultList;

            var oldListCount = (resultListObject.Value as List<CustomerVM>).Count;

            var result = await _controller.Delete(3);
            Assert.IsType<OkResult>(result);


            resultList = await _controller.List();
            resultListObject = (OkObjectResult)resultList;
            var newListCount = (resultListObject.Value as List<CustomerVM>).Count;

            Assert.Equal(oldListCount-1, newListCount);
        }

        [Fact]
        public async Task Update_should_return_error_with_invalid_id()
        {
            var model = new CustomerVM()
            {
                Id = 10,
                Name = "AbdulNaser",
                Address="China",
                Phone="0086111212222"
            };

            var result = await _controller.Update(model);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_should_return_error_with_invalid_model()
        {
            var model = new CustomerVM()
            {
                Id = 3,
                Name = "AbdulNaser",
                Address = "China"
            };

            _controller.ModelState.AddModelError("InvalidModel", "Phone is null");

            var result = await _controller.Update(model);
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task Update_should_return_success_with_valid_model()
        {
            var model = new CustomerVM()
            {
                Id = 2,
                Name = "Essam",
                Address = "Djibouti",
                Phone = "002537787877"
            };

            var result = await _controller.Update(model);
            Assert.IsType<OkResult>(result);
        }
    }
}