using LanguageExt;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.AccessCards;

namespace SmartPass.Services.Interfaces
{
    public interface IAccessCardService : IGenericCRUDService<AccessCard, Guid, AccessCardDto>
    {
        Task<Option<AccessCardWithSessionsDto>> GetAccessCardWithSessions(CancellationToken ct = default);
        Task<IEnumerable<AccessCardWithDeletedFlagDto>> GetAccessCardsWithDeletedFlag(CancellationToken ct = default);
    }
}
