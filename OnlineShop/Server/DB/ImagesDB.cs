using OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
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
            var sql = "SELECT * FROM online_shop.images WHERE OwnerID=@userId;";
            return _db.Select<UserMosaic, ColourPalette, dynamic>(sql, (m, p) =>
            {
                m.Palette = p;
                return m;
            }, new {userId});
        }

        public Task SaveMosaic(User user, UserMosaic mosaic)
        {
            var sql = @"INSERT INTO online_shop.images 
            (OwnerID, Name, Size, Image, ColorPalette, Date)
            VALUES (@OwnerID, @Name, @Size, @Image, @ColorPaletteID, @Date)";
            return _db.Query(sql, new
            {
                OwnerID = user.ID,
                Name = mosaic.Name,
                Size = mosaic.Size,
                Image = mosaic.Image,
                ColorPaletteID = mosaic.Palette.ID,
                Date = mosaic.Date
            });
        }
    }
}