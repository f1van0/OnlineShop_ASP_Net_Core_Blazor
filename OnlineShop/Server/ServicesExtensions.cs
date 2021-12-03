using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Server.DB;
using OnlineShop.Shared;

namespace OnlineShop.Server
{
    public static class ServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, DataAccess>();
            // services.AddTransient<DataBase>();

            //Добавление Синглтона MemoryUserRepository для создания автоматической привязки его ко всем нуждающимся в нём скриптам
            services.AddSingleton<IUserRepository, UserDB>();
            services.AddTransient<CatalogDB>();
        }
        
    }
}