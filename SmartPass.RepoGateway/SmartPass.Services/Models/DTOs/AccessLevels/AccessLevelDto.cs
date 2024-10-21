using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.AccessLevels
{
    public class AccessLevelDto
    {
        public AccessLevelDto(AccessLevel accessLevel) 
        {
            Id = accessLevel.Id;
            Name = accessLevel.Name;
            Description = accessLevel.Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
