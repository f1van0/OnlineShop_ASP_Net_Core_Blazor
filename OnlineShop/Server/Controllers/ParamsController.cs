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
    public class ParamsController
    {

        private readonly ParamsDB _paramsDB;
        private ILogger<ParamsController> _logger;

        //Первичная инициализация _catalogManager для дальнейшей работы
        public ParamsController(ParamsDB paramsDB, ILogger<ParamsController> logger)
        {
            _logger = logger;
            _paramsDB = paramsDB;
        }
        
        [HttpGet("[action]")]
        //С помощью GET пользователю возвращается массив паллитр
        public async Task<IEnumerable<ColorPalette>> GetColorPalettes()
        {
            return await _paramsDB.GetPalettes();
        }
        
        [HttpGet("[action]")]
        //С помощью GET пользователю возвращается массив размеров картинки
        public async Task<IEnumerable<ImageSize>> GetImageSizes()
        {
            return await _paramsDB.GetImageSizes();
        }
    }
}