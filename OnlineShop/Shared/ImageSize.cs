using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared
{
    public record ImageSize
    { 
        [Key]
        [Required]
        public int ID { get; set; }
        
        [Required]
        public int SizeX { get; set; }
        
        [Required]
       public int SizeY { get; set; }

       public string Name => SizeX + "x" + SizeY;
       public float Proportion => (float)SizeY / SizeX;
    }
}