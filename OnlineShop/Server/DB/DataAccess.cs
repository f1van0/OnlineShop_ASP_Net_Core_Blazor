using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace OnlineShop.Server.DB
{
    public class DataAccess : IDataAccess
    {
        private string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> Select<T>(string sql)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync<T>(sql);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, U>(string sql, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync<T>(sql, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, U>(string sql, Func<T, S, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, U>(string sql, Func<T, S, S1, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, U>(string sql, Func<T, S, S1, S2, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, U>(string sql, Func<T, S, S1, S2, S3, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, S4, U>(string sql, Func<T, S, S1, S2, S3, S4, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<List<T>> Select<T, S, S1, S2, S3, S4, S5, U>(string sql, Func<T, S, S1, S2, S3, S4, S5, T> mapper, U parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            var rows = await connection.QueryAsync(sql, mapper, parameters);
            return rows.ToList();
        }

        public async Task<int> Query<T>(string sql, T parameters)
        {
            await using var connection = new MySqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, parameters);
        }
    }
}