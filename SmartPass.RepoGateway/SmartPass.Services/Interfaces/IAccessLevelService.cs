using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.AccessLevels;

namespace SmartPass.Services.Interfaces
{
    public interface IAccessLevelService : IGenericCRUDService<AccessLevel, Guid, AccessLevelDto>
    {
    }
}
