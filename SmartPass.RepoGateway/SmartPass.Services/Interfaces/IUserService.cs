using LanguageExt;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Models.DTOs.Users;

namespace SmartPass.Services.Interfaces
{
    public interface IUserService : IGenericCRUDService<User, Guid, UserDto>
    {
        Task<ICollection<UserDto>> GetUsersByRole(RoleValue role, CancellationToken ct = default);
        Task<Option<UserWithCardsDto>> GetUserWithCards(Guid id, CancellationToken ct = default);
        Task<Option<UserWithSessionDto>> GetUserWithSession(Guid id, CancellationToken ct = default);
        Task<IEnumerable<UserWithDeletedFlagDto>> GetUsersWithDeletedFlag(CancellationToken ct = default);
    }
}
