using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.Users
{
    public class UserDto
    {
        public UserDto(User user)
        { 
            Id = user.Id;
            UserName = user.UserName;
            Department = user.Department;
            Description = user.Description;
            UserRoles = string.Join("|",(user.UserRoles.Select(r => r.Name)));
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserRoles { get; set; }
        public string Department { get; set; }
        public string? Description { get; set; }
    }
}
