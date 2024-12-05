namespace SmartPass.Services.Models.Requests.Users
{
    public class AddUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Department { get; set; }
        public string? Description { get; set; }
        public ICollection<String> Roles { get; set; }
    }
}
