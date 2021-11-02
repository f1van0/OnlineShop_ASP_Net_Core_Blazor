using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;

        //Первичная инициализация _userRepository для дальнейшей работы
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //С помощью POST из Body по пришедшим состороны пользователя credentials, система пытается зарегистрировать его в системе и возвращает пользователю результат
        [HttpPost]
        public ActionResult<User> Post([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.Register(credentials);
            return Ok(user);
        }

        //С помощью PUT из Body по пришедшим состороны пользователя credentials, система пытается залогинить (найти сведения, тождественные credentials) и возвращает пользователю результат
        /// <summary>
        /// Login user from credentials
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<User> Put([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.Login(credentials);
            return Ok(user);
        }
    }
}