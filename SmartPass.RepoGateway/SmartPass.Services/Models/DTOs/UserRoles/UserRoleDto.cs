using SmartPass.Repository.Models.Entities;
namespace SmartPass.Services.Models.DTOs.UserRoles
{
    public class UserRoleDto
    {
        public UserRoleDto(UserRole userRole) 
        { 
            Id = userRole.Id;
            Name = userRole.Name;
            Description = userRole.Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
