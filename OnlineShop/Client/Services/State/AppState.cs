using OnlineShop.Shared;
using System;
using System.Net.Http;

namespace OnlineShop.Client.Services.State
{
    public class AppState
    {
        public event Action StateChanged;
        public UserInfo UserState { get; private set; } = new UserInfo();
        public ColorPalettesRepository ColorPalettesRepository { get; private set; }
        public ImageSizesRepository ImageSizesRepository { get; private set; }
        public ImageRepository ImageRepository { get; private set; }

        private readonly HttpClient _client;

        public AppState(HttpClient client)
        {
            _client = client;
            ColorPalettesRepository = new ColorPalettesRepository(_client);
            ImageSizesRepository = new ImageSizesRepository(_client);
            ImageRepository = new ImageRepository(_client);
        }

        public void SetUserState(UserInfo state)
        {
            UserState = state;
            StateChanged?.Invoke();
        }
    }
}