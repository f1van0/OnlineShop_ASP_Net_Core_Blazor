using OnlineShop.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB.Mappers
{
    public class ImagesDB
    {
        public IDataAccess _db;

        public ImagesDB(IDataAccess db)
        {
            _db = db;
        }

        public Task<List<UserMosaic>> UserMosaic(int userId)
        {
            var sql = "SELECT * FROM online_shop.images WHERE imOwnerID=@userId;";
            return _db.Select<UserMosaic, ColourPalette, dynamic>(sql, (m, p) =>
            {
                m.Palette = p;
                return m;
            }, new {userId});
        }

        public Task SaveMosaic(User user, UserMosaic mosaic)
        {
            var sql = @"INSERT INTO online_shop.images 
            (imOwnerID, imName, imSize, imImage, imColorPaletteID, imDate) 
            VALUES (@OwnerID, @Name, @Size, @Image, @ColorPaletteID, @Date)";
            return _db.Query(sql, new
            {
                OwnerID = mosaic.OwnerID,
                Name = mosaic.Name,
                Size = mosaic.Size,
                Image = mosaic.Image,
                ColorPaletteID = mosaic.Palette.ID,
                Date = mosaic.Date
            });
        }
    }
}