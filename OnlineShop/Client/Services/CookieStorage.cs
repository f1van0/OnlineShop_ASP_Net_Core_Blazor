using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    public class CookieStorage
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<T> GetCookie<T>(string key)
        {
            var cookie = await _jsRuntime.InvokeAsync<T>("blazorExtensions.GetCookie", new object?[] {key});
            return cookie;
        }

        public async Task SetCookie<T>(string key, T data, int expiredDays)
        {
            await _jsRuntime.InvokeAsync<T>("blazorExtensions.SetCookie", new object?[] {key, data, expiredDays});
        }
    }
}