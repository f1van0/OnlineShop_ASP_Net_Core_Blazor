using OnlineShop.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services.State
{
    public class ImageSizesRepository
    {
        private ImageSize[] _imageSizes;
        private HttpClient _client;

        public ImageSizesRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<ImageSize[]> GetImageSizes()
        {
            _imageSizes ??= await FetchImageSizes();
            return _imageSizes;
        }

        private async Task<ImageSize[]> FetchImageSizes() =>
            await _client.GetFromJsonAsync<ImageSize[]>("api/Params/GetImageSizes");
    }
}