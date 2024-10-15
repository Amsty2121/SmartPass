using System.ComponentModel.DataAnnotations;
using SmartPass.Repository.Models.EntityInterfaces;
using SmartPass.Repository.Models.Enums;

namespace SmartPass.Repository.Models.Entities
{
    public class AccessCard : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public CardType CardType { get; set; }

        [Required]
        public Guid AccessLevelId { get; set; }

        [Required]
        public string AccessKey { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? LastUsingUtcDate { get; set; }


        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime? UpdateUtcDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }


        public User User { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
