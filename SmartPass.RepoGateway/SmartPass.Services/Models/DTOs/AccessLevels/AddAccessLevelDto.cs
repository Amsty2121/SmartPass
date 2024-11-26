namespace SmartPass.Services.Models.DTOs.AccessLevels
{
    public class AddAccessLevelDto
    {
        public string Name { get; set; }
        public bool IsForSpecificZone { get; set; }
        public string? Description { get; set; }
    }
}
