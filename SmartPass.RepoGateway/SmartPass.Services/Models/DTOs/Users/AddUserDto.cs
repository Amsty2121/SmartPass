namespace SmartPass.Services.Models.DTOs.Users
{
    public class AddUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<String> Roles { get; set; }
        public string? Department { get; set; }
        public string? Description { get; set; }
    }
}
