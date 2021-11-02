using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public record User
    {
        public Guid Id;

        public string Login;

        public string Password;

        public DateTime Registered;
        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
