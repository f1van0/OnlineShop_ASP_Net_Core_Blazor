using OnlineShop.Server.DB.Mappers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services.State
{
    public class ColorPalettesRepository
    {
        private ColorPalette[] _palettes;
        private HttpClient _client;

        public ColorPalettesRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<ColorPalette[]> GetPalettes()
        {
            _palettes ??= await FetchPalettes();
            return _palettes;
        }

        private async Task<ColorPalette[]> FetchPalettes() =>
            await _client.GetFromJsonAsync<ColorPalette[]>("api/Params/GetColorPalettes");
    }
}