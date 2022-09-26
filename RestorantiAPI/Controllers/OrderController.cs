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
            var orders = _orderService.GetListOrdersAsync().Result;
            if (orders.Count > 0)
                return Ok(orders);
            else
                return BadRequest("Nenhum pedido encontrado.");
        }

        [HttpPost]
        [Route("PostOrder")]
        public async Task<IActionResult> PostOrder([FromBody] OrderVM request)
        {
            if (ModelState.IsValid)
            {
                var Add = await _orderService.PostOrderAsync(request);
                if (Add)
                    return Ok(request);
                else
                    return BadRequest("Ocorreu um problema ao tentar gerar o pedido. Tente novamente!");
            }
            else
            {
                return BadRequest("Model is not valid!");
            }
        }
    }
}
