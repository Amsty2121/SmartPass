using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.AccessCards
{
    public class AccessCardWithDeletedFlagDto : AccessCardDto
    {
        public AccessCardWithDeletedFlagDto(AccessCard accessCard) : base(accessCard)
        {
            IsDeleted = accessCard.IsDeleted;
            DeletedUtcDate = accessCard.DeletedUtcDate;
        }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }
    }
}
