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
    [Route("api/item")]
    public class ItemController : Controller
    {
        private readonly IItemService ItemService;
        public ItemController(IItemService _ItemService)
        {
            ItemService = _ItemService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await ItemService.ListAsync();
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var result = await ItemService.GetAsync(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: ItemController/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ItemVM model)
        {
            try
            {
                await this.ItemService.AddAsync(model);
                return CreatedAtAction(nameof(GetById), new { Id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: ItemController/Edit/5
        [HttpPut]
        public async Task<IActionResult> Update(ItemVM data)
        {
            try
            {
                await ItemService.UpdateAsync(data);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: ItemController/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.ItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
