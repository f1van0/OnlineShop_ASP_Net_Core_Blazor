using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using OnlineShop.Client.Services;
using OnlineShop.Client.Services.State;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OnlineShop.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddSingleton<Navigation>();
            builder.Services.AddTransient<CookieStorage>();
            builder.Services.AddTransient<BrowserAPI>();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}