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

        public async Task<List<T>> Select<T>(string sql)
        {
            var rows = await _connection.QueryAsync<T>(sql);
            return rows.ToList();
        }
        
        public async Task<List<T>> Select<T, U>(string sql, U parameters)
        {
            var rows = await _connection.QueryAsync<T>(sql, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, U>(string sql, Func<T, S, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, U>(string sql, Func<T, S, S1, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, U>(string sql, Func<T, S, S1, S2, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, U>(string sql, Func<T, S, S1, S2, S3, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, S4, U>(string sql, Func<T, S, S1, S2, S3, S4, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, S4, S5, U>(string sql, Func<T, S, S1, S2, S3, S4, S5, T> mapper, U parameters)
        {
            var rows = await _connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }
        
        public async Task<int> Query<T>(string sql, T parameters) =>
            await _connection.ExecuteAsync(sql, parameters);
    }
}