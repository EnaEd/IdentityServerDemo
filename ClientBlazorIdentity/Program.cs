using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientBlazorIdentity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44390/") });

            builder.Services.AddOidcAuthentication(options =>
            {
                //builder.Configuration.Bind("oidc", options.ProviderOptions); doesn't work on .net 5.0.1

                options.ProviderOptions.Authority = "https://localhost:5000/";
                options.ProviderOptions.ClientId = "blazor";
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.DefaultScopes.Add("api1");
                options.ProviderOptions.DefaultScopes.Add("offline_access");

            });

            await builder.Build().RunAsync();
        }
    }
}
