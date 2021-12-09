using OnlineShop.Server.DB.Mappers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Shared
{
    [Table(("images"))]
    public record UserImage
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public int OwnerID { get; set; }
        [Required]
        public int SizeID { get; set; }
        [Required]
        public int[][] Image { get; set; }
        [Required]
        public int PaletteID { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}