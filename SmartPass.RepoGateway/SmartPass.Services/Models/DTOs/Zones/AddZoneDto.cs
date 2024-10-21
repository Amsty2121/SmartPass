namespace SmartPass.Services.Models.DTOs.Zones
{
    public class AddZoneDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AccessLevelId { get; set; }
        public string? Description { get; set; }
    }
}
