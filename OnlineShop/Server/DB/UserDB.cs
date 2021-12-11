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
            string sql = "SELECT * FROM online_shop.users WHERE UserName = @Username AND Password = @Password;";
            var users = await _db.Select<User, UserCredentials>(sql, credentials);
            return users.FirstOrDefault();
        }

        public async Task<User> Register(UserCredentials credentials)
        {
            string sql = "START TRANSACTION;" +
                         "INSERT INTO online_shop.users(Username, Password, RoleId) Values(@Username, @Password, @RoleId);" +
                         "SELECT * FROM online_shop.users WHERE ID = LAST_INSERT_ID();" +
                         "COMMIT;";
            var users = await _db.Select<User, dynamic>(sql,
                new {Username = credentials.UserName, Password = credentials.Password, RoleId = 1});
            return users.FirstOrDefault();
        }

        public async Task<bool> UserExist(string username)
        {
            string sql = "SELECT * FROM online_shop.users WHERE UserName = @Username;";
            var users = await _db.Select<User, dynamic>(sql, new {Username = username});

            if (users.FirstOrDefault() != null)
                return true;
            else
                return false;
        }

        public async Task<UserInfo> UserInfoById(int id)
        {
            string sql = "SELECT * FROM online_shop.users WHERE ID=@ID;";
            var users = await _db.Select<User, dynamic>(sql, new {ID = id});

            var user = users.FirstOrDefault();
            if (user == null)
                return null;
            
            return new UserInfo(user.UserName, user.RoleID);
        }
    }
}