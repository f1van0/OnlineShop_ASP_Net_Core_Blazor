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

        public Task<List<UserImage>> GetUserImages(int userId)
        {
            var sql = "SELECT * FROM online_shop.images WHERE OwnerID=@userId;";
            return _db.Select<UserImage, int, dynamic>(sql, (m, p) =>
            {
                m.PaletteID = p;
                return m;
            }, new {userId});
        }

        public Task SaveUserImage(int userID, UserImage image)
        {
            var sql = @"INSERT INTO online_shop.images 
            (OwnerID, Name, SizeID, Image, ColorPaletteID, Date)
            VALUES (@OwnerID, @Name, @Size, @Image, @ColorPaletteID, @Date)";
            return _db.Query(sql, new
            {
                OwnerID = userID,
                Size = image.SizeID,
                Image = image.Image,
                ColorPaletteID = image.PaletteID,
                Date = image.Date
            });
        }

        public Task SetColorPaletteOfUserImage(int userID, UserImage userImage)
        {
            var sql = @"UPDATE online_shop.images SET colorPaletteID=@ColorPaletteID WHERE ID=@ID";
            return _db.Query<dynamic>(sql, new
            {
                ColorPaletteID = userImage.PaletteID,
                ID = userID
            });
        }
    }
}