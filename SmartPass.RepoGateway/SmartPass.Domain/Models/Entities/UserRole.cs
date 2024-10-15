using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Models.EntityInterfaces;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Repository.Models.Entities
{
    public class UserRole : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }


        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime UpdateUtcDate { get; set; }


        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }


        public ICollection<User> Users { get; set; }

    }
}
