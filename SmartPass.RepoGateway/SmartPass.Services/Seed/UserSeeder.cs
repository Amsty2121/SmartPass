using SmartPass.Repository.Contexts;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Users;

namespace SmartPass.Services.Seed
{
    public static class UserSeeder
    {
        public static async Task Seed(SmartPassContext context, IUserService service)
        {
            if (context.Users.Any())
                return;

            var users = new List<AddUserDto>
            {
                new AddUserDto()
                {
                    UserName = "global.admin",
                    Password = "Qwerty1!",
                    Department = "Administration",
                    Description = "Default ADMIN USER created at first DB migration application",
                    Roles = new[] { "Admin", "DeviceUser" }
                },
                new AddUserDto()
                {
                    UserName = "jora.cardan",
                    Password = "Qwerty1!",
                    Department = "CleaningServices",
                    Description = "Default PHONE USER created at first DB migration application",
                    Roles = new[] { "DeviceUser" }
                },

            };

            foreach (var user in users)
            {
                await service.Create(user);
            }
        }
    }
}
