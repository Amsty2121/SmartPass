using SmartPass.Repository.Models.EntityInterfaces;

namespace SmartPass.Services.Models.DTOs.Sessions
{
    public class SessionWithDeletedFlagDto : SessionDto
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedUtcDate {  get; set; }
    }
}
