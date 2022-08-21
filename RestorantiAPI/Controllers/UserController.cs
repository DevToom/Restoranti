﻿using Domain.Interface;
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

        private readonly IRUserInternal _rUserInternal;
        private readonly IUserInternalService _userService;
        public UserController(IRUserInternal rUserInternal,
                              IUserInternalService userService
                              )
        {
            this._rUserInternal = rUserInternal;
            this._userService = userService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserInternal request)
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
