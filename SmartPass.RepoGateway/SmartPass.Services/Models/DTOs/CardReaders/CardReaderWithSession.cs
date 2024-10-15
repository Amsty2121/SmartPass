using SmartPass.Services.Models.DTOs.Sessions;

namespace SmartPass.Services.Models.DTOs.Devices
{
    public class CardReaderWithSession : CardReaderDto
    {
        public ICollection<SessionDto> Sessions { get; set; }
    }
}