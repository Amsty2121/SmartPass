﻿using SmartPass.Repository.Models.EntityInterfaces;
using SmartPass.Repository.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Repository.Models.Entities
{
    public class CardReader : IBaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid AccessLevelId { get; set; }

        [Required]
        public InOutFlag InOutFlag { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? LastUsingUtcDate { get; set; }

        [Required]
        public DateTime CreateUtcDate { get; set; }
        public DateTime? UpdateUtcDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }


        public AccessLevel AccessLevel { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}