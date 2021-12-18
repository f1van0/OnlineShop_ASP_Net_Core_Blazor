using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Shared
{
    public record PairID()
    {
        //[JsonPropertyName("ColorPaletteID")]
        [Required]
        public int ID1 { get; set; }
        
        //[JsonPropertyName("SizeID")]
        [Required]
        
        public int ID2 { get; set; }
        
        //[JsonPropertyName("count")]
        [Required]
        public int count { get; set; }
    }
}