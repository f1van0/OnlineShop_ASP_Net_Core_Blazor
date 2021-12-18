using OnlineShop.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB.Mappers
{
    public class OperationsDB
    {
        public IDataAccess _db;

        public OperationsDB(IDataAccess db)
        {
            _db = db;
        }
        
        public async Task<List<PurchaseGoods>> GetOperations(int userID)
        {
            //string sql = "SELECT (UserName, GoodsName, Price, Date):view_goods_operations FROM view_goods_operations WHERE UserID = @UserID";
            //var purchases = await _db.Select<PurchaseGoods, dynamic>(sql, new { UserID = userID });
            //return purchases;
            string sql = "SELECT * FROM users WHERE ID = @ID";
            var user = await _db.Select<User, dynamic>(sql, new {ID = userID});
            if (user.Count != 0)
            {
                sql = "SELECT * FROM view_goods_operations WHERE UserName = @Username";
                var purchases = await _db.Select<PurchaseGoods, dynamic>(sql, new { UserName = user[0].UserName });
                return purchases;
            }
            else
            {
                return new List<PurchaseGoods>();
            
            }
        }

        public async Task<List<ValueStats>> GetAllOperations()
        {
            var sql = "SELECT " +
                              "goods_catalog.ColorPaletteID AS ColorPaletteID, " +
                              "goods_catalog.SizeID AS SizeID, " +
                              "COUNT(*) AS count " +
                           "FROM goods_catalog " +
                              "RIGHT OUTER JOIN color_palettes " +
                                "ON goods_catalog.ColorPaletteID = color_palettes.ID " +
                              "INNER JOIN goods_operations " +
                                "ON goods_operations.GoodsID = goods_catalog.ID " +
                              "RIGHT OUTER JOIN image_sizes " +
                                "ON goods_catalog.SizeID = image_sizes.ID " +
                           "GROUP BY SizeID, ColorPaletteID ";
            return await _db.Select<ValueStats>(sql);
        }
    }
}