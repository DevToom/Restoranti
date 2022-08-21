using Domain.Interface;
using Entities.Entities;
using Infra.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IRUser _rUser;
        private readonly IUserService _userService;
        public UserController(IRUser rUser,
                              IUserService userService
                              )
        {
            this._rUser = rUser;
            this._userService = userService;
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Create(request);
                if (!result.HasError)
                    return Ok("Usuário criado com sucesso!");
                else
                    return BadRequest(result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError.ToString());
            }
        }

        [Route("GetUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.List();
            if (result?.Count > 0)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
