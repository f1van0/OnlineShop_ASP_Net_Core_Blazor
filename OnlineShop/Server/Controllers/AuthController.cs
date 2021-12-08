using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Shared;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private SymmetricSecurityKey _securityKey;

        //Первичная инициализация _userRepository для дальнейшей работы
        public AuthController(IUserRepository userRepository, SymmetricSecurityKey securityKey)
        {
            _securityKey = securityKey;
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
        public async Task<ActionResult> Post([FromBody] UserCredentials credentials)
        {
            if (_userRepository.UserExist(credentials.UserName).Result)
                return BadRequest();

            var user = await _userRepository.Register(credentials);
            if (user == null)
                return BadRequest();

            var jwtToken = NewToken(user.ID, user.RoleID == 2);


            HttpContext.Response.Headers.Append("Authorization", "Basic " + jwtToken);
            HttpContext.Response.Cookies.Append("_token", jwtToken);
            // HttpContext.Response.Cookies.Append("auth", $"{user.UserName}, {user.Password}");
            return Ok();
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
        public async Task<ActionResult> Put([FromBody] UserCredentials credentials)
        {
            var user = await _userRepository.Login(credentials);
            if (user == null)
                return Unauthorized();

            var jwtToken = NewToken(user.ID, user.RoleID == 2);

            HttpContext.Response.Headers.Append("Authorization", "Basic " + jwtToken);
            HttpContext.Response.Cookies.Append("_token", jwtToken);
            // HttpContext.Response.Cookies.Append("authID", $"{user.ID}");
            return Ok(user);
        }

        private string NewToken(int userId, bool isAdmin)
        {
            var signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            var claimsdata = new[] {new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")};

            var securityToken = new JwtSecurityToken(
                issuer: "blazor.app",
                audience: "blazor.users",
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddHours(10),
                claims: claimsdata
            );
            securityToken.Payload["userId"] = userId;

            JwtSecurityTokenHandler tokenHandler = new();
            return tokenHandler.WriteToken(securityToken);
        }
    }
}