using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public interface IDataAccess
    {
        Task<List<T>> Select<T>(string sql);
        Task<List<T>> Select<T, U>(string sql, U parameters);
        Task<int> Query<T>(string sql, T parameters);
        Task<List<T>> Select<T, S, U>(string sql, Func<T, S, T> mapper, U parameters);
        Task<List<T>> Select<T, S, S1, U>(string sql, Func<T, S, S1, T> mapper, U parameters);
        Task<List<T>> Select<T, S, S1, S2, U>(string sql, Func<T, S, S1, S2, T> mapper, U parameters);
        Task<List<T>> Select<T, S, S1, S2, S3, U>(string sql, Func<T, S, S1, S2, S3, T> mapper, U parameters);
        Task<List<T>> Select<T, S, S1, S2, S3, S4, U>(string sql, Func<T, S, S1, S2, S3, S4, T> mapper, U parameters);
        Task<List<T>> Select<T, S, S1, S2, S3, S4, S5, U>(string sql, Func<T, S, S1, S2, S3, S4, S5, T> mapper, U parameters);
    }
}
