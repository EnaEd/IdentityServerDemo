using IdentityServerDemo.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerDemo.BLL
{
    public static class Startup
    {
        public static void RegisterServiceBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServiceDAL(configuration);
        }
    }
}
