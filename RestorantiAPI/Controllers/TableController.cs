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
        [Route("Add")]
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

    }
}
