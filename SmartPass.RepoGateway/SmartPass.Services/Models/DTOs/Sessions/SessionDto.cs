using SmartPass.Repository.Models.Enums;

namespace SmartPass.Services.Models.DTOs.Sessions
{
    public class SessionDto
    {
        public Guid Id { get; set; }

        public Guid AccessCardId { get; set; }

        public Guid DeviceId { get; set; }

        public SessionStatus SessionStatus { get; set; }
        
        public string? Description { get; set; }
        
        public DateTime CreateUtcDate { get; set; }
    }
}
