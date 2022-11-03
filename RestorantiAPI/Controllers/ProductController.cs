using Domain.Interface;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRProduct _rProduct;
        public ProductController(IProductService productService, IRProduct rProduct)
        {
            this._productService = productService;
            this._rProduct = rProduct;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Add(product).Result;

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
                var result = _productService.GetList().Result;

                if (!result.HasError)
                    return Ok(result.Entity);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar os produtos. Ex: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.Update(product).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }

        [HttpDelete]
        [Route("Delete/{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            if (ModelState.IsValid)
            {
                var result = _productService.DeleteProduct(ProductId).Result;

                if (!result.HasError)
                    return Ok(result);
                else
                    return BadRequest(result);

            }
            else
                return BadRequest("Model is not valid!");
        }

        [HttpGet]
        [Route("GetImage/{Id}")]
        public async Task<IActionResult> GetImage(int Id)
        {
            try
            {
                var result = _rProduct.GetById(Id).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar a imagem do produto. Ex: {ex.Message}");
            }
        }



        [HttpGet]
        [Route("ListWithFilter")]
        public async Task<IActionResult> ListWithFilter(int CategoryId, int Status, string? Name = "")
        {
            try
            {
                var result = await _productService.GetListByFilters(Name, CategoryId, Status);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Houve um problema ao tentar buscar as categorias com filtros. Ex: {ex.Message}");
            }
        }



    }
}
