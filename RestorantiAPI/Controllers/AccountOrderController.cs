using Domain.Interface;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountOrderController : ControllerBase
    {
        private readonly IAccountOrderService _accountOrderService;
        private readonly IRAccountOrder _rAccountOrder;
        public AccountOrderController(IAccountOrderService accountOrderService, IRAccountOrder rAccountOrder)
        {
            this._accountOrderService = accountOrderService;
            this._rAccountOrder = rAccountOrder;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccountOrder accountOrder)
        {
            if (ModelState.IsValid)
            {
                var result = _accountOrderService.Add(accountOrder).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }

        
        [HttpPost]
        [Route("OpenedAccount")]
        public async Task<IActionResult> OpenedAccount(int TableNumber, int UserId)
        {
            if (ModelState.IsValid)
            {
                var result = _accountOrderService.OpenAccount(TableNumber, UserId).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }
    }
}
