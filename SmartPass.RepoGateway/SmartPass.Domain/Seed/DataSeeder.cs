using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Models.Entities;

namespace SmartPass.Repository.Seed
{
    public static class DataSeeder
    {
        public static async Task Seed(UserManager<User> userManager)
        {
            if (userManager.Users.Any())
                return;

            var admins = new List<User>
                {
                    new User()
                    {
                        UserName = "admin",
                        Department = "Administration",
                        Description = "Default ADMIN USER created at first DB migration application"
                    }
                };

            foreach (var admin in admins)
            {
                if (userManager.CreateAsync(admin, "Qwerty1!").Result.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            var users = new List<User>
                {
                    new User
                    {
                        UserName = "jora.cardan",
                        Department = "CleaningServices",
                        Description = "Default PHONE USER created at first DB migration application"
                    }
                };

            foreach (var user in users)
            {
                if (userManager.CreateAsync(user, "Qwerty1!").Result.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "PhoneUser");
                }
            }
        }
    }
}
