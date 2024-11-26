using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessCards;
using SmartPass.Services.Models.DTOs.Users;
using SmartPass.Services.Models.Requests.AccessCards;
using SmartPass.Services.Models.Resposes.AccessCards;
using System.Data.SqlTypes;
using System.Security.Cryptography;

namespace SmartPass.Services.Implementations
{
    public class AccessCardService(IGenericRepository<SmartPassContext, AccessCard> accessCardRepo, 
                                   IGenericRepository<SmartPassContext, User> userRepo,
                                   IGenericRepository<SmartPassContext, AccessLevel> accessLevelRepo) : IAccessCardService
    {
        private IGenericRepository<SmartPassContext, AccessCard> AccessCardRepo { get; } = accessCardRepo;
        private IGenericRepository<SmartPassContext, User> UserRepo { get; } = userRepo;
        private IGenericRepository<SmartPassContext, AccessLevel> AccessLevelRepo { get; } = accessLevelRepo;

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

        public async Task<Result<AddAccessCardResponse>> Create(AddAccessCardRequest request, CancellationToken ct = default)
        {
            var user = await UserRepo.GetById(request.UserId, ct);
            if (user is null)
                return new Result<AddAccessCardResponse>(new KeyNotFoundException($"NotFoundException: Insertion failed - UserId {request.UserId} not found"));

            var cardType = request.CardType;
            var cardState = request.CardState;

            var accessLevel = await AccessLevelRepo.GetById(request.AccessLevelId, ct);
            if (accessLevel is null)
                return new Result<AddAccessCardResponse>(new KeyNotFoundException($"NotFoundException: Insertion failed - AccessLevelId {request.AccessLevelId} not found"));

            var accessCard = new AccessCard
            {
                Id = Guid.NewGuid(),
                PassKeys = Convert.ToBase64String(RandomNumberGenerator.GetBytes(60)),
                PassIndex = 1,
                UserId = request.UserId,
                CardType = cardType,
                CardState = cardState,
                AccessLevelId = request.AccessLevelId,
                Description = request.Description,
                CreateUtcDate = DateTime.UtcNow,
            };

            var result = await AccessCardRepo.Add(accessCard, ct);
            return result > 0 ? new AddAccessCardResponse(accessCard) : new Result<AddAccessCardResponse>(new SqlTypeException($"DbException:Insertion failed - Access Card fro User {user.UserName}"));
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

        public async Task<Option<GetMyAccessCardsMobileResponse>> GetAllByUserIdMobile(Guid userId, CancellationToken ct = default)
        {
            var user = await UserRepo.GetById(userId, ct);

            if(user == null)
                return null;

            var accessCards = await AccessCardRepo.GetWhereWithInclude(x => x.UserId == userId, ct, x => x.AccessLevel);

            return new GetMyAccessCardsMobileResponse(accessCards.Select(x => new AccessCardDto(x)), user);
        }
    }


    //credittxnedit - nu se mapeaza original duration si de asta nu vine e subfield 

}
