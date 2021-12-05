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
        
        public async Task<List<GoodsStats>> GetGoods()
        {
            string sql = "SELECT * FROM goods_catalog";
            var goods = await _db.Select<GoodsStats, GoodsStats>(sql, null);
            return goods;
        }
        
        public async Task BuyGoods(int userID, int goodsID)
        {
            string sql = "INSERT INTO goods_operations(UserID, GoodsID) Values(@UserID, @GoodsID);";
            var buy = await _db.Query<dynamic>(sql, new { UserID = userID, GoodsID = goodsID});
        }
    }
}