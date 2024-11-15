using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessCards;

namespace SmartPass.Services.Implementations
{
    public class AccessCardService(IGenericRepository<SmartPassContext, AccessCard> accessCardRepo) : IAccessCardService
    {
        private IGenericRepository<SmartPassContext, AccessCard> AccessCardRepo { get; } = accessCardRepo;
        
        public async Task<Option<AccessCardDto>> Get(Guid id, CancellationToken ct = default)
        {
            var accessCard = await AccessCardRepo.GetByIdWithInclude(id, ct, ac => ac.User);
            return accessCard is null ? null : new AccessCardDto(accessCard);
        }
        public async Task<IEnumerable<AccessCardDto>> GetAll(CancellationToken ct = default)
        {
            var accessCards = await AccessCardRepo.GetAllWithInclude(ct, ac => ac.User);
            return !accessCards.Any() ? Enumerable.Empty<AccessCardDto>() : accessCards.Select(ac => new AccessCardDto(ac));
        }

        public Task<Result<AccessCardDto>> Create(AddAccessCardDto addDto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AccessCardDto>> Update(UpdateAccessCardDto updateDto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AccessCardDto>> Delete(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AccessCardDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccessCardToMobileDto>> GetAllByUserId(Guid userId, CancellationToken ct = default)
        {
            var accessCards = await AccessCardRepo.GetWhereWithInclude(x => x.UserId == userId, ct, x => x.User, x => x.AccessLevel);
            return !accessCards.Any() ? Enumerable.Empty<AccessCardToMobileDto>() : accessCards.Select(x => new AccessCardToMobileDto(x));
        }
    }
}
