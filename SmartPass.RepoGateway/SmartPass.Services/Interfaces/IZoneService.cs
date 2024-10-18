using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.Zones;

namespace SmartPass.Services.Interfaces
{
    public interface IZoneService : IGenericCRUDService<Zone, Guid, ZoneDto>
    {
    }
}
