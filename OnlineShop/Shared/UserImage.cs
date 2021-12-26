using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineShop.Shared
{
    public record UserImage
    {
        [Key]
        [Required]
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [Required]
        [JsonPropertyName("ownerID")]
        public int OwnerID { get; set; }
        [Required]
        [JsonPropertyName("sizeID")]
        public int SizeID { get; set; }
        [Required]
        [JsonPropertyName("image")]
        public int[][] Image { get; set; }
        [Required]
        [JsonPropertyName("paletteID")]
        public int ColorPaletteID { get; set; }
        [Required]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}