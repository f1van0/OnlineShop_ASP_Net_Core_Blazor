using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using System.Threading.Tasks;

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
        public async Task<ActionResult<User>> Post([FromBody] UserCredentials credentials)
        {
            bool isExists = await _userRepository.UserExist(credentials.UserName);
            if (isExists)
                return BadRequest();

            User user = await _userRepository.Register(credentials);
            if (user == null)
                return BadRequest();

            HttpContext.Response.Cookies.Append("auth", $"{user.UserName}, {user.Password}");
            HttpContext.Response.Cookies.Append("authID", $"{user.ID}");
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
        public async Task<ActionResult<User>> Put([FromBody] UserCredentials credentials)
        {
            User user = await _userRepository.Login(credentials);
            if (user == null)
                return Unauthorized();

            HttpContext.Response.Cookies.Append("authID", $"{user.ID}");
            return Ok(user);
        }
    }
}