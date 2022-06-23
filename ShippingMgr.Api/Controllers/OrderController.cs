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
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderService OrderService;
        public OrderController(IOrderService _OrderService)
        {
            OrderService = _OrderService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await OrderService.ListAsync();
                return Ok(result);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await OrderService.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(OrderVM model)
        {
            try
            {
                await this.OrderService.AddAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderVM data)
        {
                try
               {
                   await OrderService.UpdateAsync(data);
                   return Ok();
               }
               catch(Exception ex)
               {
                   return BadRequest(ex.Message);
               }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            
            try
            {
                await this.OrderService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
