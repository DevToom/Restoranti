using Domain.Interface;
using Entities.Entities;
using Entities.Entities.VM;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IROrder _rOrder;
        private readonly IOrderService _orderService;
        public OrderController(IROrder rOrder,
                              IOrderService orderService
                              )
        {
            this._rOrder = rOrder;
            this._orderService = orderService;
        }

        [HttpGet]
        [Route("GetListOrder")]
        public async Task<IActionResult> GetListOrder()
        {
            var result = _orderService.GetListOrdersAsync().Result;

            if (!result.HasError)
                return Ok(result.Entity);
            else
                return BadRequest(result);
        }

        [HttpPost]
        [Route("PostOrder")]
        public async Task<IActionResult> PostOrder([FromBody] OrderVM request)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.PostOrderAsync(request);
                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            else
            {
                return BadRequest("Model is not valid!");
            }
        }
    }
}
