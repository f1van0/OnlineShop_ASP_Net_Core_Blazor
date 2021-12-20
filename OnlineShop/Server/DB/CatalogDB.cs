using OnlineShop.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public class CatalogDB
    {
        public IDataAccess _db;

        public CatalogDB(IDataAccess db)
        {
            _db = db;
        }

        public async Task<List<GoodsStats>> GetGoods(GoodsFilter filter)
        {
            string sql;
            List<GoodsStats> goods;
            
            if (filter.ColorPalette == null)
            {
                if (filter.ImageSize == null)
                {
                    if (filter.Name == null)
                    {
                        sql = "SELECT * FROM goods_catalog";
                        goods = await _db.Select<GoodsStats>(sql);
                    }
                    else
                    {
                        sql = "SELECT * FROM goods_catalog WHERE Name LIKE @Name";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {Name = '%' + filter.Name + '%'});
                    }
                }
                else
                {
                    if (filter.Name == null)
                    {
                        sql = "SELECT * FROM goods_catalog WHERE SizeID = @SizeID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {SizeID = filter.ImageSize.ID});
                    }
                    else
                    {
                        sql = "SELECT * FROM goods_catalog WHERE Name LIKE @Name AND SizeID = @SizeID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {Name = '%' + filter.Name + '%', SizeID = filter.ImageSize.ID});
                    }
                }
            }
            else
            {
                if (filter.ImageSize == null)
                {
                    if (filter.Name == null)
                    {
                        sql = "SELECT * FROM goods_catalog WHERE ColorPaletteID = @ColorPaletteID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {ColorPaletteID = filter.ColorPalette.ID});
                    }
                    else
                    {
                        sql = "SELECT * FROM goods_catalog WHERE Name LIKE @Name AND ColorPaletteID = @ColorPaletteID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {Name = '%' + filter.Name + '%', ColorPaletteID = filter.ColorPalette.ID});
                    }
                }
                else
                {
                    if (filter.Name == null)
                    {
                        sql = "SELECT * FROM goods_catalog WHERE ColorPaletteID = @ColorPaletteID AND SizeID = @SizeID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {ColorPaletteID = filter.ColorPalette.ID, SizeID = filter.ImageSize.ID});
                    }
                    else
                    {
                        sql = "SELECT * FROM goods_catalog WHERE Name LIKE @Name AND ColorPaletteID = @ColorPaletteID AND SizeID = @SizeID";
                        goods = await _db.Select<GoodsStats, dynamic>(sql, new {Name = '%' + filter.Name + '%', ColorPaletteID = filter.ColorPalette.ID, SizeID = filter.ImageSize.ID});
                    }
                }
            }
   //     if (filter.ColorPalette == null && filter?.ImageSize == null)
   //         {
   //             sql = "SELECT * FROM goods_catalog";
   //             goods = await _db.Select<GoodsStats, GoodsStats>(sql, null);
   //         }
   //         else if (filter.ColorPalette != null && filter.ImageSize == null)
   //         {
   //             sql = "SELECT * FROM goods_catalog WHERE ColorPaletteID = @ColorPaletteID;";
   //             goods = await _db.Select<GoodsStats, dynamic>(sql, new {ColorPaletteID = filter.ColorPalette.ID});
   //         }
   //         else if (filter.ColorPalette == null)
   //         {
   //             sql = "SELECT * FROM goods_catalog WHERE SizeID = @SizeID;";
   //             goods = await _db.Select<GoodsStats, dynamic>(sql, new {SizeID = filter.ImageSize.ID});
   //         }
   //         else
   //         {
   //             sql = "SELECT * FROM goods_catalog WHERE ColorPaletteID = @ColorPaletteID AND SizeID = @SizeID;";
   //             goods = await _db.Select<GoodsStats, dynamic>(sql, new {ColorPaletteID = filter.ColorPalette.ID, SizeID = filter.ImageSize.ID});
   //         }
            return goods;
        }
        
        public async Task BuyGoods(int userID, int goodsID)
        {
            string sql = "INSERT INTO goods_operations(UserID, GoodsID) Values(@UserID, @GoodsID);";
            var buy = await _db.Query<dynamic>(sql, new { UserID = userID, GoodsID = goodsID});
        }
    }
}