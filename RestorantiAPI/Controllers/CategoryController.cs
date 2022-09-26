using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        public CategoryController()
        {}

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetList()
        {

            return Ok();
        }
    }
}
