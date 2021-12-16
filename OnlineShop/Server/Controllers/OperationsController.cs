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

        [HttpPost("[action]")]
        //С помощью GET пользователю возвращается список его операций
        public async Task<ActionResult<PurchaseGoods[]>> GetUserOperations([FromBody] int uselessInfo)
        {
            //JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
            //var payload = jwtSecurityToken.GetPayload<JWTPayload>();
            var operations = await _operationsDb.GetOperations(uselessInfo);
            
            if (operations.Count != 0)
            {
                return Ok(operations);
            }
            else
            {
                return NotFound(null);
            }
        }
    }
}