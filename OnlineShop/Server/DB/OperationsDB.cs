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
    }
}