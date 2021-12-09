using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Server.DB.Mappers
{
    public class ColorPalette
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color1 { get; set; }
        [Required]
        public string Color2 { get; set; }
        [Required]
        public string Color3 { get; set; }
        [Required]
        public string Color4 { get; set; }
        [Required]
        public string Color5 { get; set; }
        [Required]
        public string Color6 { get; set; }
        [Required]
        public string Color7 { get; set; }
    }
}