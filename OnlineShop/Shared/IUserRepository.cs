using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public interface IUserRepository
    {
        Task<User> Register(UserCredentials credentials);

        Task<User> Login(UserCredentials credentials);

        Task<bool> UserExist(string username);
        
        Task<UserInfo> UserInfoById(int id);
    }
}
