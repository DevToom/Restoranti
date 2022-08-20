using Entities.Entities;
using Infra.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace RestorantiAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IRUser _rUser;
        public UserController(IRUser rUser)
        {
            this._rUser = rUser;
        }

        [HttpPost]
        public IEnumerable<WeatherForecast> Login([FromBody] User request)
        {

            _rUser.Add(request);

            return null;
        }


    }
}
