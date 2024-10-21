namespace SmartPass.Services.Models.DTOs.Users
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? Department { get; set; }
        public string? Description { get; set; }
    }
}
