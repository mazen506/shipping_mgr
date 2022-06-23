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
    [Route("api/currencies")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService currencyService;
        public CurrencyController(ICurrencyService _currencyService)
        {
            currencyService = _currencyService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var result = await currencyService.ListAsync();
                return Ok(result);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(long id)
        {
            var result =  await currencyService.GetAsync(id);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);
        }


        // POST: CurrencyController/Create
        [HttpPost("Create")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(CurrencyVM model)
        {
            try
            {
                await currencyService.AddAsync(model);
                return CreatedAtAction( nameof(GetById), new { id = model.Id }, model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: CurrencyController/Edit/5
        [HttpPut]
        public async Task<IActionResult> Update(CurrencyVM model)
        {
            try
            {
                await currencyService.UpdateAsync(model);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
