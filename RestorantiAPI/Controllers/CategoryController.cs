using Domain.Interface;
using Entities.Entities;
using Infra.Repository;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IRCategory _rCategory;
        public CategoryController(ICategoryService categoryService, IRCategory rCategory)
        {
            this._categoryService = categoryService;
            this._rCategory = rCategory;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Update(category).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }
        
        [HttpDelete]
        [Route("Delete/{CategoryId}")]
        public async Task<IActionResult> Delete(int CategoryId)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.DeleteCategory(CategoryId).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _categoryService.GetList().Result;

                if (!result.HasError)
                    return Ok(result.Entity);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as categorias. Ex: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetImage/{Id}")]
        public async Task<IActionResult> GetImage(int Id)
        {
            try
            {
                var result = _rCategory.GetById(Id).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as categorias. Ex: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListWithFilter")]
        public async Task<IActionResult> ListWithFilter(int MenuType, int Status, string? Name = "")
        {
            try
            {
                var result = await _categoryService.GetListByFilters(Name,MenuType,Status);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as categorias com filtros. Ex: {ex.Message}");
            }
        }


    }
}
