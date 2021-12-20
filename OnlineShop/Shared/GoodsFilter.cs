using OnlineShop.Server.DB.Mappers;

namespace OnlineShop.Shared
{
    public record GoodsFilter
    {
        public string Name { get; set; }
        public ColorPalette ColorPalette { get; set; }
        public ImageSize ImageSize { get; set; }
    }
}