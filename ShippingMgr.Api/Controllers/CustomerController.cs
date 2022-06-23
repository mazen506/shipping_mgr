using DotNetCore.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShippingMgr.Core.Application.Interfaces;
using ShippingMgr.Core.Models;
using ShippingMgr.Core.ViewModels;

namespace ShippingMgr.Controllers
{

    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService CustomerService;
        public CustomerController(ICustomerService _CustomerService)
        {
            CustomerService = _CustomerService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await CustomerService.ListAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await CustomerService.GetAsync(id);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CustomerController/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CustomerVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                await this.CustomerService.AddAsync(model);
                return CreatedAtAction(nameof(GetById), new { Id = model.Id }, model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerVM data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                var result = await this.CustomerService.UpdateAsync(data);
                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: CustomerController/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var result = await this.CustomerService.DeleteAsync(id);
                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
