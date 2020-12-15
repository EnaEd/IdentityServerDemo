using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IdentityServerDemo.Presentation.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    Config.Config.Clients.ToList().ForEach(client => context.Clients.Add(client.ToEntity()));
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    Config.Config.ApiScopes.ToList().ForEach(resource => context.ApiScopes.Add(resource.ToEntity()));
                    context.SaveChanges();
                }

                //Add other resources here

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in Config.IdentityResources)
                //    {
                //        context.IdentityResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}
            }
        }
    }
}
