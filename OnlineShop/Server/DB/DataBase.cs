using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;
using OnlineShop.Shared;

namespace OnlineShop.Server.DB
{
    public class DataBase
    {
        IDataAccess db;

        public DataBase(IDataAccess connection)
        {
            this.db = connection;
        }

        public async Task<DateTime> GetTime()
        {
            var time = await db.Select<DateTime, int?>("select now()", null);
            return time.First();
        }

        public async Task AddUser(string username, string password)
        {
            string sql = "INSERT INTO users(username, password) Values(@username, @password)";
            await db.Query<User>(sql, new User{ Username = username, Password = password });
        }
    }
}
