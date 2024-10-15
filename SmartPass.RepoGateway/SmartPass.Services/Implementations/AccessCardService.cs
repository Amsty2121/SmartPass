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
    public class AccessCardService(IGenericRepository<SmartPassContext, AccessCard> accessCardRepo,
                                   UserManager<User> userRepository) : IAccessCardService
    {
        private IGenericRepository<SmartPassContext, AccessCard> AccessCardRepo { get; } = accessCardRepo;
        public UserManager<User> UserRepository { get; } = userRepository;

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

        public async Task<Result<AccessCardDto>> Create(AccessCard entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
            /*var user = 

            var accessCards = await AccessCardRepo.
            entity.Id = Guid.NewGuid();
            var result = await AccessCardRepo.Add(entity, ct);*/
            
        }

        public Task<Result<AccessCardDto>> Delete(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        
        public Task<IEnumerable<AccessCardWithDeletedFlagDto>> GetAccessCardsWithDeletedFlag(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Option<AccessCardWithSessionsDto>> GetAccessCardWithSessions(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        

        public Task<Result<AccessCardDto>> Update(AccessCard entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AccessCardDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
