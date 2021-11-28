using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public record UserCredentials
    {
        [Required]
        [StringLength(maximumLength: 12, MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
