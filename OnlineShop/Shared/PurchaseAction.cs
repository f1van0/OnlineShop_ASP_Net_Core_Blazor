namespace OnlineShop.Server.Controllers
{
    public record PurchaseAction
    {
        public int GoodsId { get; set; }
        public int UserId { get; set; }
    }
}