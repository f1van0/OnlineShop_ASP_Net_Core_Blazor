using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public record User
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
        [Key]
        [Required]
        [StringLength(maximumLength: 12, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 16, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Timestamp]
        public DateTime Date { get; set; }

        public User()
        {
            //Id = Guid.NewGuid();
            //Registered = DateTime.Now;
        }
    }
}
