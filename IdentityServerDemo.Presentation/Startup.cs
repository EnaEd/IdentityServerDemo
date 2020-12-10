using IdentityServerDemo.BLL;
using IdentityServerDemo.Presentation.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IdentityServerDemo.Presentation
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServiceBLL(_configuration);
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
            //.AddTestUsers(TestUsers)
            .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder => builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                  sql => sql.MigrationsAssembly(migrationsAssembly)))

            .AddOperationalStore(options =>
            options.ConfigureDbContext = builder => builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly)));

            //.AddInMemoryApiScopes(Config.Config.ApiScopes)
            //.AddInMemoryClients(Config.Config.GetClients());



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.InitializeDatabase();

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod());

            app.UseIdentityServer();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
