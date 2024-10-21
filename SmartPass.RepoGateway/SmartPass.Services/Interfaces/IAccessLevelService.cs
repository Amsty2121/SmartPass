using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.AccessLevels;

namespace SmartPass.Services.Interfaces
{
    public interface IAccessLevelService
    {
        Task<Option<AccessLevelDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<AccessLevelDto>> GetAll(CancellationToken ct = default);


        Task<Result<AccessLevelDto>> Create(AddAccessLevelDto entity, CancellationToken ct = default);
        Task<Result<AccessLevelDto>> Update(UpdateAccessLevelDto entity, CancellationToken ct = default);
        Task<Result<AccessLevelDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<AccessLevelDto>> DeleteSoft(Guid id, CancellationToken ct = default);
    }
}
