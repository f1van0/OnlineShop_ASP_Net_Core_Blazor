namespace OnlineShop.Client.Services.State
{
    public record UserState
    {
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}