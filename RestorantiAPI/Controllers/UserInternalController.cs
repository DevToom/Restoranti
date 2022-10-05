using Domain.Interface;
using Entities.Entities;
using Entities.Entities.VM;
using Infra.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInternalController : ControllerBase
    {

        private readonly IRUserInternal _rUserInternal;
        private readonly IUserInternalService _userService;
        public UserInternalController(IRUserInternal rUserInternal,
                              IUserInternalService userService
                              )
        {
            this._rUserInternal = rUserInternal;
            this._userService = userService;
        }

        #region Login

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
                return BadRequest("Model is not valid!");
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserInternal request)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(request);
                if (!result.HasError)
                    return Ok(result.User);
                else
                    return BadRequest(result.Message);
            }
            else
            {
                return BadRequest("Model is not valid!");
            }
        }

        #endregion

        #region RecoveryPassword

        [Route("recoverypassword/validate")]
        [HttpPost]
        public async Task<IActionResult> Validate([FromBody] UserValidateRecoveryPassword request)
        {
            var user = await _rUserInternal.GetByUsername(new UserInternal { Username = request.Username });
            if (user != null)
            {
                if (user.Email == request.Email)
                    return Ok();
                else
                    return BadRequest("Email não encontrado!");
            }
            else
                return BadRequest("Usuário não existente!");

        }

        [Route("recoverypassword/validatepasswordconfirm/{password}")]
        [HttpPost]
        public async Task<IActionResult> ValidatePasswordConfirm(string password)
        {
            var isvalid = _userService.ValidatePasswordConfirm(password).Result;

            if (isvalid)
                return Ok();
            else
                return BadRequest("Senha inválida!");
        }

        [Route("recoverypassword/updatepasswordconfirm")]
        [HttpPost]
        public async Task<IActionResult> UpdatePassWordConfirm([FromBody] UserValidateRecoveryPassword request)
        {
            var isUpdate = _userService.UpdatePasswordViaRecovery(request).Result;

            if (isUpdate)
                return Ok("Senha alterada com sucesso.");
            else
                return BadRequest("Ocorreu um problema ao tentar atualizar a senha. Tente novamente!");
        }

        #endregion

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
