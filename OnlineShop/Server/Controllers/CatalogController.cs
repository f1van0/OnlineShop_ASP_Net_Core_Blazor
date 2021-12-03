using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using OnlineShop.Server.DB;
using System.Collections.Generic;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogDB _catalogDb;

        //Первичная инициализация _catalogManager для дальнейшей работы
        public CatalogController(CatalogDB catalogDb)
        {
            _catalogDb = catalogDb;
        }

        [HttpGet]
        //С помощью GET пользователю возвращается список товаров
        public IEnumerable<GoodsStats> Get()
        {
            return _catalogDb.GetGoods().Result.ToArray();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        //С помощью POST из Body по пришедшим со стороны пользователя ID товара и ID пользователя, система пытается
        //добавить товар в список покупок
        public ActionResult<bool> Post([FromBody] PurchaseAction action)
        {
            if (_catalogDb.BuyGoods(action.GoodsId, action.UserId).IsCompleted)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }
    }
}