using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers.OnlineShop.Server.DB.Mappers;
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
            services.AddTransient<ImagesDB>();
        }

        public static void AddDapperMappers(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler(new ArrayMapper<Color>());
            SqlMapper.AddTypeHandler(new ArrayMapper<int>());
            SqlMapper.AddTypeHandler(new ArrayMapper<int[]>());
            SqlMapper.AddTypeHandler(new StringsMapper());
        }
    }
}