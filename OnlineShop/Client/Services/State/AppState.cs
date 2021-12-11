using OnlineShop.Shared;
using System.Net.Http;

namespace OnlineShop.Client.Services.State
{
    public class AppState
    {
        HttpClient _client;

        public AppState(HttpClient client)
        {
            _client = client;
            ColorPalettesRepository = new ColorPalettesRepository(_client);
            ImageSizesRepository = new ImageSizesRepository(_client);
            ImageRepository = new ImageRepository(_client);
        }

        public UserInfo UserState { get; set; } = new UserInfo();
        public ColorPalettesRepository ColorPalettesRepository { get; set; }
        public ImageSizesRepository ImageSizesRepository { get; set; }
        public ImageRepository ImageRepository { get; set; }
    }
}