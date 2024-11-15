using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using SmartPass.Repository.Interfaces;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Seed;

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
                    var context = services.GetRequiredService<SmartPassContext>();
                    var userService = services.GetRequiredService<IUserService>();
                    var userRoleService = services.GetRequiredService<IUserRoleService>();
                    await context.Database.MigrateAsync();

                    await RoleSeeder.Seed(context, userRoleService);
                    await UserSeeder.Seed(context, userService);
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
