using System.ComponentModel.DataAnnotations;

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
