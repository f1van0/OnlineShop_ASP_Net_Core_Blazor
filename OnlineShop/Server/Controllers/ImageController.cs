using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Shared;
using OnlineShop.Server.DB;
using OnlineShop.Server.Services;
using System;
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
        public async Task<ActionResult<UserImage>> UploadImage([FromBody] SaveImageRequest image)
        {
            JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
            var payload = jwtSecurityToken.GetPayload<JWTPayload>();
            int userId = payload.UserId;
            // if (image.Size.SizeX != image.Pixels[0].Length || image.Size.SizeY != image.Pixels.Length)
            //     return BadRequest();

            if (userId != -1)
            {
                try
                {
                    _logger.LogInformation("[ImagesDB] Try to add new image {0}", image);
                    UserImage userImage = await _imagesDb.SaveUserImage(userId, image.Pixels, paletteId: image.Palette.ID, sizeId: image.Size.ID);
                    _logger.LogInformation("[ImagesDB] Added new image {0}", userImage);
                    
                    if (userImage != null)
                        return Ok(userImage);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception while inserting image to DB");
                
                }

                return StatusCode(500);
            }

            return Unauthorized();
        }

        // [Authorize]
        // [HttpPut("[action]")]
        // //С помощью GET пользователю возвращается список его картинок
        // public async Task<ActionResult<IEnumerable<UserImage>>> GetImages()
        // {
        //     JwtSecurityToken jwtSecurityToken = HttpContext.Request.GetToken();
        //     var payload = jwtSecurityToken.GetPayload<JWTPayload>();
        //     if (payload.UserId != -1)
        //     {
        //         var userImages = await _imagesDb.GetUserImages(payload.UserId);
        //         return Ok(userImages);
        //     }
        //     else
        //     {
        //         return Unauthorized(null);
        //     }
        // }

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