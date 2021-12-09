using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Shared;
using OnlineShop.Server.DB;
using OnlineShop.Server.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace OnlineShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
                private readonly ImagesDB _imagesDb;
                private ILogger<ImageController> _logger;
        
                //Первичная инициализация _catalogManager для дальнейшей работы
                public ImageController(ImagesDB imagesDB, ILogger<ImageController> logger)
                {
                    _logger = logger;
                    _imagesDb = imagesDB;
                }
                
                [Authorize]
                [HttpGet("[action]")]
                //С помощью GET пользователю возвращается список его картинок
                public async Task<IEnumerable<UserImage>> GetImages()
                {
                    JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
                    var payload = jwtSecurityToken.GetPayload<JWTPayload>();
                    if (payload.UserId != -1)
                    {
                        var userImages = await _imagesDb.GetUserImages(payload.UserId);
                        return userImages;
                    }
                    else
                    {
                        return null;
                    }
                }
                
                [Authorize]
                [HttpPost("[action]")]
                //С помощью POST из Body по пришедшей со стороны пользователя картинке, система пытается
                //добавить её в список картинок
                public ActionResult<UserImage> UploadImage([FromBody] UserImage userImage)
                {
                    JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
                    var payload = jwtSecurityToken.GetPayload<JWTPayload>();
                    if (payload.UserId != -1)
                    {
                        // if (_catalogDb.BuyGoods(payload.UserId, goodsID).IsCompleted)
                        // {
                        //     return Ok(ResponseStatus.Completed);
                        // }
                        // else
                        // {
                        //     return BadRequest(ResponseStatus.Failed);
                        // }
                    }

                    return Unauthorized();
                }
                
                [Authorize]
                [HttpPost("[action]")]
                //С помощью POST из Body по пришедшей со стороны пользователя картинке с измененным цветом, система пытается
                //изменить цвет у заданной картинки
                public ActionResult<UserImage> SetImageColor([FromBody] UserImage userImage)
                {
                    JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
                    var payload = jwtSecurityToken.GetPayload<JWTPayload>();
                    if (payload.UserId != -1)
                    {
                        _imagesDb.SetColorPaletteOfUserImage(payload.UserId, userImage);
                    }

                    return Unauthorized();
                }
    }
}