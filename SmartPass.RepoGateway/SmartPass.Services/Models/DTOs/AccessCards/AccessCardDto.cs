using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;

namespace SmartPass.Services.Models.DTOs.AccessCards
{
    public class AccessCardDto
    {
        public AccessCardDto(AccessCard accessCard)
        {
            Id = accessCard.Id;
            UserId = accessCard.UserId;
            UserName = accessCard.User.UserName;
            CardType = accessCard.CardType;
            AccessLevelId = accessCard.AccessLevelId;
            AccessLevelName = accessCard.AccessLevel.Name;
            Description = accessCard.Description;
            LastUsingUtcDate = accessCard.LastUsingUtcDate;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public CardType CardType { get; set; }

        public Guid AccessLevelId { get; set; }
        public string AccessLevelName { get; set; }

        public string? Description { get; set; }

        public DateTime? LastUsingUtcDate { get; set; }
    }
}
