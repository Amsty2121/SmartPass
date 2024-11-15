using LanguageExt.Pipes;
using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Implementations;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPass.Services.Seed
{
    public class RoleSeeder
    {
        public static async Task Seed(SmartPassContext context, IUserRoleService userRoleService)
        {
            if (context.UserRoles.Any())
                return;

            var roles = new List<AddUserRoleDto>
            {
                new AddUserRoleDto
                {
                    Name = "Admin",
                    Description = "Admin user Role",
                },
                new AddUserRoleDto
                {
                    Name = "DeviceUser",
                    Description = "DeviceUser user Role",
                }
            };

            foreach (var role in roles)
            {
                await userRoleService.Create(role);
            }
        }
    }
}
