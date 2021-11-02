using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public record UserCredentials
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
