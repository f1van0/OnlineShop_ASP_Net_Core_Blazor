using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public interface IDataAccess
    {
        Task<List<T>> Select<T, U>(string sql, U parameters);
        Task<int> Query<T>(string sql, T parameters);
    }
}
