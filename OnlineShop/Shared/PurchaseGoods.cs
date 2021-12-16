using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Shared
{
    public record PurchaseGoods
    {
        [Required]
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }
        
        [Required]
        [JsonPropertyName("GoodsName")]
        public string GoodsName { get; set; }

        [Required]
        [JsonPropertyName("Price")]
        public int Price { get; set; }
        
        [Required]
        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }
    }
}