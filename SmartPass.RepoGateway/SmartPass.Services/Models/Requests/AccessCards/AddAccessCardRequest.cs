using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;

namespace SmartPass.Services.Models.Requests.AccessCards
{
    public class AddAccessCardRequest
    {
        public Guid UserId { get; set; }
        public CardType CardType { get; set; }
        public CardState CardState { get; set; }
        public Guid AccessLevelId { get; set; }
        public string? Description { get; set; }

        public AccessCard MapToBase()
            => new AccessCard
            {
                UserId = UserId,
                CardType = CardType,
                CardState = CardState,
                AccessLevelId = AccessLevelId, 
                Description = Description
            };
    }
}
