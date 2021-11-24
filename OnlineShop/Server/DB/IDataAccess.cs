using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public interface IDataAccess
    {
        Task<List<T>> Select<T>(string sql, object parameters);
        Task Query(string sql, object parameters);
    }
}
