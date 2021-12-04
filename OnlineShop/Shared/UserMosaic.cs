using OnlineShop.Server.DB.Mappers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Shared
{
    [Table(("images"))]
    public record UserMosaic
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
        public Vector Size { get; set; }
        public int[][] Image { get; set; }
        public ColourPalette Palette { get; set; }
        public DateTime Date { get; set; }
    }
}