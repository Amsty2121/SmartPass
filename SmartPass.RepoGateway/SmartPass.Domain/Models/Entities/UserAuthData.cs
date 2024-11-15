using SmartPass.Repository.Models.EntityInterfaces;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Repository.Models.Entities
{
    public class UserAuthData : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string RefreshToken { get; set; }
        public DateTime ExpiresUtc { get; set; }


        public DateTime CreateUtcDate { get; set; }
        public DateTime? UpdateUtcDate { get; set; }

        
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }

        public User User { get; set; }
    }
}
