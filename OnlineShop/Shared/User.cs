using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public record User
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public int RoleID { get; set; }

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
