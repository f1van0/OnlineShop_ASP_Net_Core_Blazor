using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public class GoodsStats
    {
        [Key] [Required] public int ID { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public int Price { get; set; }
        
        [Required] public string ImageSize { get; set; }

        [Required] public int ColorPaletteID { get; set; }

        [Required] public string Description { get; set; }
    }
}