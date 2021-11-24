using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using Dapper;

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
            var time = await db.Select<DateTime>("select now()", null);
            return time.First();
        }
    }
}
