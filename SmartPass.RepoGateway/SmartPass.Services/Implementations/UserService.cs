using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.Identity;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Users;
using System.Data;
using System.Transactions;
using SmartPass.Services.CustomErrors;
using OneOf;

namespace SmartPass.Services.Implementations
{
    public class UserService(UserManager<User> userManager,
                             IGenericRepository<SmartPassContext, User> userRepository) : IUserService
    {
        public UserManager<User> UserManager { get; } = userManager;
        private IGenericRepository<SmartPassContext, User> UserRepository { get; } = userRepository;

        public async Task<Option<UserDto>> Get(Guid id, CancellationToken ct = default)
        {
            var user = await UserRepository.GetById(id, ct);
            return user is null ? null : new UserDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll(CancellationToken ct = default)
        {
            var users = await UserRepository.GetAll(ct);
            return !users.Any() ? Enumerable.Empty<UserDto>() : users.Select(u => new UserDto(u));
        }

        Task<Result<UserDto>> Create(User entity, CancellationToken ct)
        {
            throw new NotImplementedException();
        }


        public Task<Result<UserDto>> Delete(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        

        

        public Task<ICollection<UserDto>> GetUsersByRole(RoleValue role, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserWithDeletedFlagDto>> GetUsersWithDeletedFlag(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Option<UserWithCardsDto>> GetUserWithCards(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Option<UserWithSessionDto>> GetUserWithSession(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> Update(User entity, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        Task<Result<UserDto>> IGenericCRUDService<User, Guid, UserDto>.Create(User entity, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
