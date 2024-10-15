using SmartPass.Repository.Models.EntityInterfaces;
using SmartPass.Repository.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Repository.Models.Entities
{
    public class Session : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AccessCardId { get; set; }

        public Guid DeviceId { get; set; }


        [Required]
        public SessionStatus SessionStatus { get; set; }
        public string? Description { get; set; }


        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime? UpdateUtcDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }


        public CardReader CardReader { get; set; }
        public AccessCard AccessCard { get; set; }
    }
}
