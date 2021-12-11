namespace OnlineShop.Shared
{
    public record UserInfo
    {
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserInfo() { }

        public UserInfo(string nickname, int roleId)
        {
            UserName = nickname;
            Role = roleId switch
            {
                1 => "User",
                2 => "Admin",
                _ => "Unknown"
            };
        }
    }
}