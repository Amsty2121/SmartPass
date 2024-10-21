using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Sessions;

namespace SmartPass.Services.Implementations
{
    public class SessionService(IGenericRepository<SmartPassContext, Session> sessionRepo) : ISessionService
    {
        public IGenericRepository<SmartPassContext, Session> SessionRepo { get; } = sessionRepo;

        public Task<Result<SessionDto>> Create(AddSessionDto addDto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SessionDto>> Delete(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SessionDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Option<SessionDto>> Get(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SessionDto>> GetAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        Task<Result<SessionDto>> ISessionService.Update(UpdateSessionDto updateDto, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
