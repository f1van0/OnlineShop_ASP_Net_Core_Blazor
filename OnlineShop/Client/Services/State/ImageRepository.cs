using OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services.State
{
    public class ImageRepository
    {
        public UserImage[] Images { get; } = Array.Empty<UserImage>();

        private HttpClient _client;

        public ImageRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserImage> UploadImage(int[][] pixels, ImageSize size, ColorPalette palette)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync("/api/Image/UploadImage",
                new SaveImageRequest() {Palette = palette, Pixels = pixels, Size = size});

            if (responseMessage.IsSuccessStatusCode)
            {
                var image =  await responseMessage.Content.ReadFromJsonAsync<UserImage>();
                Images.Append(image);
                return image;
            }

            return null;
        }
    }
}