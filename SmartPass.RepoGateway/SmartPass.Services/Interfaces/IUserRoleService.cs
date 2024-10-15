using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.UserRoles;

namespace SmartPass.Services.Interfaces
{
    public interface IUserRoleService : IGenericCRUDService<UserRole, Guid, UserRoleDto>
    {
        
    }
}
