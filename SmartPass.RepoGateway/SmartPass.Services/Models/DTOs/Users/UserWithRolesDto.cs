using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.Users
{
    public class USerWithRolesDto : UserDto
    {
        public USerWithRolesDto(User user) : base(user)
        {
            UserRoles = string.Join("|", (user.UserRoles.Select(r => r.Name)));
        }

        public string UserRoles { get; set; }
    }
}
