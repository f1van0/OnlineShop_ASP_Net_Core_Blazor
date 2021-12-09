using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using OnlineShop.Server.DB;
using OnlineShop.Server.DB.Mappers;
using OnlineShop.Server.DB.Mappers.OnlineShop.Server.DB.Mappers;
using OnlineShop.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;

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
            services.AddTransient<ParamsDB>();
        }

        public static void AddDapperMappers(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler(new ArrayMapper<Color>());
            SqlMapper.AddTypeHandler(new ArrayMapper<int>());
            SqlMapper.AddTypeHandler(new ArrayMapper<int[]>());
            SqlMapper.AddTypeHandler(new StringsMapper());
        }
    }

    public static class HTTPExtensions
    {
        public static JwtSecurityToken GetToken(this HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"].First();
            JwtSecurityToken securityToken = new JwtSecurityTokenHandler().ReadJwtToken(authHeader.Split(" ").Last());
            return securityToken;
        }
    }


    public static class JWTExtensions
    {
        public static T GetPayload<T>(this JwtSecurityToken securityToken)
        {
            var content = (string)securityToken.Payload["content"];
            return JsonSerializer.Deserialize<T>(content);
        }

        public static void SetPayload<T>(this JwtSecurityToken securityToken, T content)
        {
            var str = JsonSerializer.Serialize(content);
            securityToken.Payload["content"] = str;
        }
    }
}