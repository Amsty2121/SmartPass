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
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? Department { get; set; }
        public string? Description { get; set; }
    }
}
