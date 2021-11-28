using OnlineShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Server.DB
{
    public class UserDB : IUserRepository
    {
        public IDataAccess _db;

        public UserDB(IDataAccess db)
        {
            _db = db;
        }

        public async Task<User> Login(UserCredentials credentials)
        {
            string sql = "SELECT * FROM users WHERE username = @Username AND password = @Password;";
            var users = await _db.Select<User, User>(sql, new User { Username = credentials.UserName, Password = credentials.Password });
            return users.FirstOrDefault();
        }

        public async Task<User> Register(UserCredentials credentials)
        {
            string sql = "START TRANSACTION;" +
                "INSERT INTO users(username, password) Values(@username, @password);"+
                "SELECT * FROM users WHERE ID = LAST_INSERT_ID();"+
                "COMMIT;";
            var users = await _db.Select<User, User>(sql, new User { Username = credentials.UserName, Password = credentials.Password});
            return users.FirstOrDefault();
        }

        public async Task<bool> UserExist(string login)
        {
            string sql = "SELECT * FROM users WHERE username = @Username;";
            var users = await _db.Select<User, dynamic>(sql, new { Username = login });
            
            if (users.FirstOrDefault() != null)
                return true;
            else
                return false;
        }
    }
}
