using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineShop.Server.DB;
using System;
using System.IO;
using System.Reflection;
using System.Data.Common;
using System.Text;
using System.Text.Unicode;

namespace OnlineShop.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DbConnection>((provider) =>
            {
                var conn = new MySql.Data.MySqlClient.MySqlConnection() {ConnectionString = Configuration.GetConnectionString("MySql")};
                conn.Open();
                return conn;
            });

            string securityPrivateKey = Configuration.GetValue<string>("Auth:PrivateKey");
            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityPrivateKey));
            services.AddSingleton(secKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "blazor.app",
                        ValidAudience = "blazor.users",
                        IssuerSigningKey = secKey
                    });


            services.RegisterServices();
            services.AddDapperMappers();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Добавление Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "OnlineShop.Api", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ImagesDB imgDB, ILogger<Startup> logger)
        {
            // Add Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop.API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "static-files")),
                RequestPath = "/static",
            });

            app.UseRouting();

            var enabledAuth = Configuration.GetValue<bool?>("Auth:Enable") ?? false;
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (enabledAuth)
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllers();
                }
                else
                {
                    endpoints.MapRazorPages().WithMetadata(new AllowAnonymousAttribute());
                    endpoints.MapControllers().WithMetadata(new AllowAnonymousAttribute());
                }
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}