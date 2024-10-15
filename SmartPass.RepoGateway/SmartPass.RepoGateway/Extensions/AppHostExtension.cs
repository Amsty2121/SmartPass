using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Seed;
using SmartPass.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace SmartPass.RepoGateway.Extensions
{
    public static class AppHostExtension
    {
        public static async void SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //var context = services.GetRequiredService<IdentityContext>();
                    var context = services.GetRequiredService<Repository.Contexts.SmartPassContext>();
                    var identityContext = services.GetRequiredService<UserManager<User>>();
                    var roles = services.GetRequiredService<RoleManager<UserRole>>();

                    await context.Database.MigrateAsync();

                    //await RoleSeeder.Seed(roles);
                    await DataSeeder.Seed(identityContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }
    }
}
