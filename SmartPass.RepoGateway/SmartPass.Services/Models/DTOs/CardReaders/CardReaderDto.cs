using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartPass.Services.Models.DTOs.CardReaders
{
    public class CardReaderDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid AccessLevelId { get; set; }

        public InOutFlag InOutFlag { get; set; }

        public string? Description { get; set; }

        public DateTime? LastUsingUtcDate { get; set; }
    }
}
