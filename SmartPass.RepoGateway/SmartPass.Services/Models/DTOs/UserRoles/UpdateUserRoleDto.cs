namespace SmartPass.Services.Models.DTOs.UserRoles
{
    public class UpdateUserRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
