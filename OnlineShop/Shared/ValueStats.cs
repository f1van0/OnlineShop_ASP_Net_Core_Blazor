using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    public record ValueStats
    {
        [JsonPropertyName("ColorPaletteID")]
        [Required]
        public int ColorPaletteID { get; set; }
        
        [JsonPropertyName("SizeID")]
        [Required]
        public int SizeID { get; set; }

        [JsonPropertyName("count")]
        [Required]
        public int count { get; set; }
    }
}
