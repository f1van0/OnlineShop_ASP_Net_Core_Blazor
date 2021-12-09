using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using OnlineShop.Shared;
using OnlineShop.Server.DB;
using OnlineShop.Server.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDB _catalogDb;
        private ILogger<CatalogController> _logger;

        //Первичная инициализация _catalogManager для дальнейшей работы
        public CatalogController(CatalogDB catalogDb, ILogger<CatalogController> logger)
        {
            _logger = logger;
            _catalogDb = catalogDb;
        }

        [HttpGet]
        //С помощью GET пользователю возвращается список товаров
        public IEnumerable<GoodsStats> Get()
        {
            return _catalogDb.GetGoods().Result.ToArray();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        //С помощью POST из Body по пришедшим со стороны пользователя ID товара и ID пользователя, система пытается
        //добавить товар в список покупок
        public ActionResult<ResponseStatus> Post([FromBody] int goodsID)
        {
            _logger.LogInformation("test");
            JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
            var payload = jwtSecurityToken.GetPayload<JWTPayload>();
            if (payload.UserId != -1)
            {
                if (_catalogDb.BuyGoods(payload.UserId, goodsID).IsCompleted)
                {
                    return Ok(ResponseStatus.Completed);
                }
                else
                {
                    return BadRequest(ResponseStatus.Failed);
                }
            }

            return Ok(ResponseStatus.NotAuthorized);
        }
    }
}