using OnlineShop.Server.DB.Mappers;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public class SaveImageRequest
    {
        [Required] public int[][] Pixels { get; set; }

        [Required] public ImageSize Size { get; set; }

        [Required] public ColorPalette Palette { get; set; }
    }
}