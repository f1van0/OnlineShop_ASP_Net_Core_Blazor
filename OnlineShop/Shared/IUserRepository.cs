using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public interface IUserRepository
    {
        User Register(UserCredentials credentials);

        User Login(UserCredentials credentials);

        bool UserExist(string login);
    }
}
