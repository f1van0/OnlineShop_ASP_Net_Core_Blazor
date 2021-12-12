using OnlineShop.Shared;
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
        
        public async Task<PurchaseGoods[]> GetOperations(int userID)
        {
            string sql = "SELECT * FROM users WHERE ID = @ID";
            var userName = await _db.Select<string, dynamic>(sql, new {ID = userID});
            sql = "SELECT * FROM view_goods_operations WHERE UserName = @Username";
            var purchases = await _db.Select<PurchaseGoods, dynamic>(sql, new {UserName = userName});
            return purchases.ToArray();
        }
    }
}