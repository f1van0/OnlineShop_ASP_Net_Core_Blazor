using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace OnlineShop.Server.DB
{
    public class DataAccess : IDataAccess
    {
        DbConnection _connection;

        public DataAccess(DbConnection connection)
        {
            this._connection = connection;
        }

        public async Task<List<T>> Select<T>(string sql, object parameters)
        {
            var rows = await _connection.QueryAsync<T>(sql, parameters);
            return rows.ToList();
        }

        public async Task Query(string sql, object parameters) => 
            await _connection.ExecuteAsync(sql, parameters);

    }
}
