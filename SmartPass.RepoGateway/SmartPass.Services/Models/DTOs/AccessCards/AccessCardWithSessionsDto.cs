using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.AccessCards
{
    public class AccessCardWithSessionsDto : AccessCardDto
    {
        public AccessCardWithSessionsDto(AccessCard accessCard) : base(accessCard)
        {
            Sessions = accessCard.Sessions;
        }

        public ICollection<AccessLog> Sessions { get; set; }
    }
}
