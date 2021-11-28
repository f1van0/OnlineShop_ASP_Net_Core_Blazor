using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        //Первичная инициализация _userRepository для дальнейшей работы
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Registers user with credentials
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>User's data with cookie</returns>
        /// <responce code="200">Returns user data</responce>
        /// <responce code="400">Returns if user credentials are empty or already in use</responce>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        //С помощью POST из Body по пришедшим состороны пользователя credentials, система пытается
        // зарегистрировать его в системе и возвращает пользователю результат
        public ActionResult<User> Post([FromBody] UserCredentials credentials)
        {
            if (_userRepository.UserExist(credentials.UserName).Result)
                return BadRequest();

            var user = _userRepository.Register(credentials);
            if (user.Result == null)
                return BadRequest();

            HttpContext.Response.Cookies.Append("auth", $"{user.Result.Username}, {user.Result.Password}");
            return Ok(user);
        }

        /// <summary>
        /// Login user from credentials
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>User's data with cookie</returns>
        /// <responce code="200">Returns user data</responce>
        /// <responce code="401">Returns if user credentials don't exist</responce>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        //С помощью PUT из Body по пришедшим со стороны пользователя credentials, система пытается
        // залогинить (найти сведения, тождественные credentials) и возвращает пользователю результат
        public ActionResult<User> Put([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.Login(credentials);
            if (user.Result == null)
                return Unauthorized();

            HttpContext.Response.Cookies.Append("auth", $"{user.Result.Username}, {user.Result.Password}");
            return Ok(user);
        }
    }
}