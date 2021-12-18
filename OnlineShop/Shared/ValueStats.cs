using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.Shared
{
    record ValueStats
    {
        [JsonPropertyName("id")]
        [Required]
        int ID { get; set; }

        [JsonPropertyName("count")]
        [Required]
        int count { get; set; }
    }
}
