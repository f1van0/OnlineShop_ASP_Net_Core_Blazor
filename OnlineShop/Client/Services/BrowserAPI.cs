using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    public class BrowserAPI
    {
        private readonly IJSRuntime _jsRuntime;

        public BrowserAPI(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task PrintPageDialog()
        {
            await _jsRuntime.InvokeAsync<string>("window.print");
        }
    }
}