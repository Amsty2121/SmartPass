using SmartPass.Repository.Contexts;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.Requests.Users;

namespace SmartPass.Services.Seed
{
    public static class UserSeeder
    {
        public static async Task Seed(SmartPassContext context, IUserService service)
        {
            if (context.Users.Any())
                return;

            var users = new List<AddUserRequest>
            {
                new AddUserRequest()
                {
                    UserName = "global.admin",
                    Password = "Qwerty1!",
                    Department = "Administration",
                    Description = "Default ADMIN USER created at first DB migration application",
                    Roles = new[] { "Admin", "DeviceUser" }
                },
                new AddUserRequest()
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
