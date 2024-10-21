using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.AccessCards;

namespace SmartPass.Services.Interfaces
{
    public interface IAccessCardService : IGenericCRUDService<AccessCard, Guid, AccessCardDto>
    {
        Task<Option<AccessCardDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<AccessCardDto>> GetAll(CancellationToken ct = default);


        Task<Result<AccessCardDto>> Create(AddAccessCardDto entity, CancellationToken ct = default);
        Task<Result<AccessCardDto>> Update(UpdateAccessCardDto entity, CancellationToken ct = default);
        Task<Result<AccessCardDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<AccessCardDto>> DeleteSoft(Guid id, CancellationToken ct = default);

        //Task<Option<AccessCardWithSessionsDto>> GetAccessCardWithSessions(CancellationToken ct = default);
        //Task<IEnumerable<AccessCardWithDeletedFlagDto>> GetAccessCardsWithDeletedFlag(CancellationToken ct = default);
    }
}
