using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public User Post([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.Register(credentials);
            return user;
        }

        //С помощью PUT из Body по пришедшим состороны пользователя credentials, система пытается залогинить (найти сведения, тождественные credentials) и возвращает пользователю результат
        [HttpPut]
        public User Put([FromBody] UserCredentials credentials)
        {

            var user = _userRepository.Login(credentials);
            return user;
        }
    }
}
