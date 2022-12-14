using Domain.Interface;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly IRTable _rTable;
        private readonly ITableService _tableService;
        public TableController(IRTable rTable,
                              ITableService tableService
                              )
        {
            this._rTable = rTable;
            this._tableService = tableService;
        }

        /// <summary>
        /// Método de criação de uma nova mesa - CADASTRO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Table request)
        {
            if (ModelState.IsValid)
            {
                request.ModifiedDate = null;
                request.ModifiedUserId = null;

                var result = await _tableService.Add(request);
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

        /// <summary>
        /// retorna a lista de mesas cadastradas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _tableService.GetList().Result;

                if (!result.HasError)
                    return Ok(result.Entity);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as mesas. Ex: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListWithFilter")]
        public async Task<IActionResult> ListWithFilter(int TableNumber, string? Status = "")
        {
            try
            {
                var result = await _tableService.GetListByFilters(TableNumber, Status);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as mesas com filtros. Ex: {ex.Message}");
            }
        }

        /// <summary>
        /// Método de atualização de um status da mesa pelo APP do caixa - atualização interna
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Table request)
        {
            if (ModelState.IsValid)
            {
                var result = await _tableService.UpdateStatusTableViaCaixa(request);
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

        /// <summary>
        /// Método de atualização de um status da mesa pelo APP do caixa, caso a mesa esteja em uso.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ConfirmUpdate")]
        public async Task<IActionResult> ConfirmUpdate([FromBody] Table request)
        {
            if (ModelState.IsValid)
            {
                var result = await _tableService.ConfirmUpdateStatusTableViaCaixa(request);
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

        /// <summary>
        /// Método para deletar uma mesa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{TableId}")]
        public async Task<IActionResult> Delete(int TableId)
        {
            if (ModelState.IsValid)
            {
                var result = _tableService.Delete(TableId).Result;

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
