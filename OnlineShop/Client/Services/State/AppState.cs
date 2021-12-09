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
        }

        public UserState UserState { get; set; } = new UserState();
        public ColorPalettesRepository ColorPalettesRepository { get; set; }
        public ImageSizesRepository ImageSizesRepository { get; set; }
    }
}