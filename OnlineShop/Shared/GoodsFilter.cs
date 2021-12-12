using OnlineShop.Server.DB.Mappers;

namespace OnlineShop.Shared
{
    public record GoodsFilter()
    {
        public ColorPalette ColorPalette { get; set; }
        public ImageSize ImageSize { get; set; }
    }
}