using OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public class ParamsDB
    {
            public IDataAccess _db;

            public ParamsDB(IDataAccess db)
            {
                _db = db;
            }

            public async Task<ColorPalette[]> GetPalettes()
            {
                string sql = "SELECT * FROM online_shop.color_palettes";
                var colorPalettes = await _db.Select<ColorPalette>(sql);
                return colorPalettes.ToArray();
            }

            public async Task<ImageSize[]> GetImageSizes()
            {
                string sql = "SELECT * FROM online_shop.image_sizes";
                var imageSizes = await _db.Select<ImageSize>(sql);
                return imageSizes.ToArray();
            }

            public async Task<bool> PaletteExists(int paletteID)
            {
                string sql = "SELECT * FROM online_shop.color_palettes WHERE ID = @ID;";
                var colorPalette = await _db.Select<ColorPalette, dynamic>(sql, new {ID = paletteID});

                if (colorPalette.FirstOrDefault() != null)
                    return true;
                else
                    return false;
            }
            
            public async Task<bool> ImageSizeExists(int sizeID)
            {
                string sql = "SELECT * FROM online_shop.image_sizes WHERE ID = @ID;";
                var imageSize = await _db.Select<ColorPalette, dynamic>(sql, new {ID = sizeID});

                if (imageSize.FirstOrDefault() != null)
                    return true;
                else
                    return false;
            }
    }
}