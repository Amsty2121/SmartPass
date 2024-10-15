using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.Sessions;

namespace SmartPass.Services.Interfaces
{
    public interface ISessionService : IGenericCRUDService<Session, Guid, SessionDto>
    {
        Task<IEnumerable<SessionWithDeletedFlagDto>> GetSessionsWithDeletedFlag(CancellationToken ct = default);
    }
}
