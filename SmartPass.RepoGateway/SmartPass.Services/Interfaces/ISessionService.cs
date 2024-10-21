using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.Sessions;

namespace SmartPass.Services.Interfaces
{
    public interface ISessionService
    {
        Task<Option<SessionDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<SessionDto>> GetAll(CancellationToken ct = default);


        Task<Result<SessionDto>> Create(AddSessionDto addDto, CancellationToken ct = default);
        Task<Result<SessionDto>> Update(UpdateSessionDto updateDto, CancellationToken ct = default);
        Task<Result<SessionDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<SessionDto>> DeleteSoft(Guid id, CancellationToken ct = default);
        
        //Task<IEnumerable<SessionWithDeletedFlagDto>> GetSessionsWithDeletedFlag(CancellationToken ct = default);
    }
}
