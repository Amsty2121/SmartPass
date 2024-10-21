namespace SmartPass.Services.Models.DTOs.AccessLevels
{
    public class UpdateAccessLevelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
