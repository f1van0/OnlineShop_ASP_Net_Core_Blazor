using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public record PurchaseGoods
    {
        [Required] public string UserName { get; set; }
        
        [Required] public int GoodsName { get; set; }

        [Required] public string Price { get; set; }
        
        [Required] [Timestamp] public DateTime Date { get; set; }
    }
}