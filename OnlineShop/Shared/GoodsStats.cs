using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Shared
{
    public class GoodsStats
    {
        [JsonPropertyName("id")]
        [Key] [Required] public int ID { get; set; }

        [JsonPropertyName("name")]
        [Required] public string Name { get; set; }
        
        [JsonPropertyName("sizeId")]
        [Required] public int SizeID { get; set; }

        [JsonPropertyName("colorPaletteId")]
        [Required] public int ColorPaletteID { get; set; }
        
        [JsonPropertyName("Price")]
        [Required] public int Price { get; set; }
    }
}