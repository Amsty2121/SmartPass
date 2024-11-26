using SmartPass.Repository.Contexts;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessLevels;
using SmartPass.Services.Models.DTOs.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPass.Services.Seed
{
    public static class AccessLevelSeeder
    {
        public static async Task Seed(SmartPassContext context, IAccessLevelService service)
        {
            if (context.AccessLevels.Any())
                return;

            var levels = new List<AddAccessLevelDto>
            {
                new AddAccessLevelDto
                {
                    Name = "Blocked",
                    Description = "Basic AccessLevel for blocked Cards/Zones",
                },new AddAccessLevelDto
                {
                    Name = "Guest",
                    Description = "Basic Lowest active role",
                },
                new AddAccessLevelDto
                {
                    Name = "Employee",
                    Description = "Basic Emp AccessLevel",
                },
                new AddAccessLevelDto
                {
                    Name = "TechnicalEmployee",
                    Description = "Basic Technical Emp AccessLevel",
                },
                new AddAccessLevelDto
                {
                    Name = "Admin",
                    Description = "Basic Almost Max AccessLevel",
                },
                new AddAccessLevelDto
                {
                    Name = "God",
                    Description = "Basic Max AccessLevel",
                },
                new AddAccessLevelDto
                {
                    Name = "Merlin's Team",
                    IsForSpecificZone = true,
                    Description = "Custom AccessLevel for ",
                }
            };

            foreach (var level in levels)
            {
                await service.Create(level);
            }
        }
    }
}
