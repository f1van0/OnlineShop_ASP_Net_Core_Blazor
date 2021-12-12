using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers;
using OnlineShop.Server.Services;
using OnlineShop.Shared;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly OperationsDB _operationsDb;
        private ILogger<OperationsController> _logger;

        //Первичная инициализация _catalogManager для дальнейшей работы
        public OperationsController(OperationsDB operationsDb, ILogger<OperationsController> logger)
        {
            _logger = logger;
            _operationsDb = operationsDb;
        }

        [Authorize]
        [HttpPut]
        //С помощью GET пользователю возвращается список его операций
        public async Task<ActionResult<PurchaseGoods[]>> Put()
        {
            JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
            var payload = jwtSecurityToken.GetPayload<JWTPayload>();
            if (payload.UserId != -1)
            {
                var result = _operationsDb.GetOperations(payload.UserId);
                if (result.IsCompleted)
                {
                    return Ok(result.Result);
                }
                else
                {
                    return BadRequest(null);
                }
            }

            return Unauthorized(null);
        }
    }
}