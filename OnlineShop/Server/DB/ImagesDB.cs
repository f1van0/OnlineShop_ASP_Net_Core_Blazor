﻿using OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _db.Select<UserImage, dynamic>(sql, new {userId});
        }

        public async Task<UserImage> SaveUserImage(int userId, int[][] pixels, int paletteId, int sizeId)
        {
            string sql = "START TRANSACTION;" +
                         @"INSERT INTO online_shop.images 
                            (OwnerID, Name, SizeID, Image, ColorPaletteID, Date)
                            VALUES (@OwnerID, @Name, @Size, @Image, @ColorPaletteID, @Date);" +
                         "SELECT * FROM online_shop.images  WHERE ID = LAST_INSERT_ID();" +
                         "COMMIT;";
            List<UserImage> userImages = await _db.Select<UserImage, dynamic>(sql, new
            {
                OwnerID = userId,
                Name = "123",
                Image = pixels,
                Size = sizeId,
                ColorPaletteID = paletteId,
                Date = DateTime.Now
            });

            return userImages.FirstOrDefault();
        }

        public Task SetColorPaletteOfUserImage(int userID, UserImage userImage)
        {
            var sql = @"UPDATE online_shop.images SET colorPaletteID=@ColorPaletteID WHERE ID=@ID";
            return _db.Query<dynamic>(sql, new {ColorPaletteID = userImage.PaletteID, ID = userID});
        }
    }
}