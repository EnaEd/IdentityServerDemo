using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ClientBlazor.Wasm
{
    public static class Startup
    {

        public static void ConfigureServices(this IServiceCollection services)
        {
            //var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44390")

            });

        }
    }
}
