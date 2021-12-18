using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public record UserCredentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
