using SmartPass.Repository.Models.EntityInterfaces;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Repository.Models.Entities
{
    public class User : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }



        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime? UpdateUtcDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }


        public ICollection<AccessCard> AccessCards { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
