using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddControllersWithViews();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()

            //.AddConfigurationStore(options =>
            //    options.ConfigureDbContext = builder => builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            //      sql => sql.MigrationsAssembly(migrationsAssembly)))

            //.AddOperationalStore(options =>
            //options.ConfigureDbContext = builder => builder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            //        sql => sql.MigrationsAssembly(migrationsAssembly)));

            .AddInMemoryApiScopes(Config.Config.ApiScopes)
            .AddInMemoryIdentityResources(Config.Config.Ids)
            //.AddInMemoryApiResources(Config.Config.Apis)
            .AddInMemoryClients(Config.Config.Clients)
            .AddTestUsers(Config.Config.Users);




        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.InitializeDatabase();

            app.UseCors(builder =>
            builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod());



            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
