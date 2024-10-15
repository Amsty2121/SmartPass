using SmartPass.Repository.Models.EntityInterfaces;

namespace SmartPass.Services.Models.DTOs.Devices
{
    public class CardReaderWithDeletedFlagDto : CardReaderDto
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }
    }
}
