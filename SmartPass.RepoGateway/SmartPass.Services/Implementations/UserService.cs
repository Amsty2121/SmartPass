using LanguageExt;
using LanguageExt.Common;
using LanguageExt.Pipes;
using Microsoft.Extensions.Options;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.AuthConfig;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Users;
using SmartPass.Services.Models.Requests.Users;
using SmartPass.Services.Models.Resposes.Users;
using SmartPass.Services.Utility;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Cryptography;

namespace SmartPass.Services.Implementations
{
    public class UserService : IUserService
    {
        private IGenericRepository<SmartPassContext, User> UserRepository { get; }
        public IGenericRepository<SmartPassContext, UserRole> UserRoleRepository { get; }
        private IGenericRepository<SmartPassContext, UserAuthData> UserAuthDataRepository { get; }
        private AuthOptions AuthOptions { get; }

        public UserService(
            IGenericRepository<SmartPassContext, User> userRepository,
            IGenericRepository<SmartPassContext, UserRole> userRoleRepository,
            IGenericRepository<SmartPassContext, UserAuthData> userAuthDataRepository,
            IOptions<AuthOptions> authOptions)
        {
            UserRepository = userRepository;
            UserRoleRepository = userRoleRepository;
            UserAuthDataRepository = userAuthDataRepository;
            AuthOptions = authOptions.Value;
        }

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

        public async Task<Result<UserDto>> Create(AddUserRequest request, CancellationToken ct)
        {
            var users = await UserRepository.GetWhere(x => x.UserName.Equals(request.UserName), ct);

            if (users.Any())
            {
                return new Result<UserDto>(new DuplicateNameException($"DuplicateException: Insertion failed - User {request.UserName} already exists"));
            }

            var roles = await UserRoleRepository.GetWhere(x => request.Roles.Contains(x.Name));

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Department = request.Department,
                Description = request.Description,
                CreateUtcDate = DateTime.UtcNow,
                IsDeleted = false,
                Password = PasswordUtility.HashPassword(request.Password),
                UserRoles = roles.ToArray(),
            };
            var result = await UserRepository.Add(user, ct);

            return result > 0 ? new UserDto(user) : new Result<UserDto>(new SqlTypeException($"DbException:Insertion failed - User {request.UserName}"));
        }

        public async Task<Result<UserDto>> Delete(Guid id, CancellationToken ct = default)
        {
            var user = await UserRepository.GetById(id, ct);

            if (user is null)
            {
                return new Result<UserDto>(new KeyNotFoundException($"NotFoundException: Deletion failed - Not found User with id {id}")); ;
            }

            var result = await UserRepository.Remove(user, ct);

            return result > 0 ? new UserDto(user) : new Result<UserDto>(new SqlTypeException($"DbException: Deletion failed - User with id {id}"));
        }

        public async Task<Result<UserDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            var user = await UserRepository.GetById(id, ct);

            if (user is null)
            {
                return new Result<UserDto>(new KeyNotFoundException($"NotFoundException: Soft Deletion failed - Not found User with id {id}")); ;
            }

            user.IsDeleted = true;
            user.DeletedUtcDate = user.UpdateUtcDate = DateTime.UtcNow;
            var result = await UserRepository.Update(user, ct);

            return result > 0 ? new UserDto(user) : new Result<UserDto>(new SqlTypeException($"DbException: Soft Deletion failed - User with id {id}"));
        }

        public async Task<Result<UserDto>> Update(UpdateUserDto entity, CancellationToken ct = default)
        {
            var user = await UserRepository.GetById(entity.Id, ct);

            if (user is null)
            {
                return new Result<UserDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found User with id {entity.Id}")); ;
            }

            var toChange = false;

            if (!string.IsNullOrWhiteSpace(entity.Description))
            {
                user.Description = entity.Description;
                toChange = true;
            }

            if (!string.IsNullOrWhiteSpace(entity.Department))
            {
                user.Department = entity.Department;
                toChange = true;
            }

            if (!toChange)
            {
                return new Result<UserDto>(new ArgumentException($"DbException: Update failed - Nothing to update for User with id {entity.Id}"));
            }

            user.UpdateUtcDate = DateTime.UtcNow;
            var result = await UserRepository.Update(user, ct);

            return result > 0 ? new UserDto(user) : new Result<UserDto>(new SqlTypeException($"DbException: Update failed - User with id {entity.Id}"));
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

        public async Task<Option<UserLoginResponse>> Login(UserLoginRequest request, CancellationToken ct = default)
        {
            var user = (await UserRepository.GetWhereWithInclude(
                x => x.UserName.Equals(request.Login) 
                && x.Password.Equals(PasswordUtility.HashPassword(request.Password)),
                ct, 
                x => x.UserRoles)).SingleOrDefault();

            if (user == null)
                return null;

            var authData = await UserAuthDataRepository.GetWhere(x => x.UserId == user.Id, ct);

            var userAuthData = TokenGenerationUtility.GenerateRefreshToken(authData.FirstOrDefault(), user.Id);

            if (authData.Any())
            {
                userAuthData.UpdateUtcDate = DateTime.UtcNow;
                await UserAuthDataRepository.Update(userAuthData, ct);
            }
            else
            {
                userAuthData.CreateUtcDate = DateTime.UtcNow;
                await UserAuthDataRepository.Add(userAuthData, ct);
            }

            return new UserLoginResponse
            {
                Token = TokenGenerationUtility.GenerateJwtToken(AuthOptions, user),
                RefreshToken = userAuthData.RefreshToken,   
            };
        }

        public async Task<Option<UserLoginResponse>> RefreshToken(string refreshToken, CancellationToken ct = default)
        {
            var authData = (await UserAuthDataRepository.GetWhere(x => x.RefreshToken.Equals(refreshToken), ct)).FirstOrDefault();

            if ( authData is null )
            {
                return null;
            }

            var user = (await UserRepository.GetWhereWithInclude(
                x => x.Id == authData.UserId,
                ct,
                x => x.UserRoles)).SingleOrDefault();

            if (user == null)
                return null;

            var userAuthData = TokenGenerationUtility.GenerateRefreshToken(authData, user.Id);

            userAuthData.UpdateUtcDate = DateTime.UtcNow;
            await UserAuthDataRepository.Update(userAuthData, ct);

            return new UserLoginResponse
            {
                Token = TokenGenerationUtility.GenerateJwtToken(AuthOptions, user),
                RefreshToken = userAuthData.RefreshToken,
            };
        }

        public async Task<Option<IsUserSynchronizedResponse>> IsUserSynchronized(Guid id, CancellationToken ct = default)
        {
            var user = await UserRepository.GetById(id, ct);
            return user is null ? null : new IsUserSynchronizedResponse(user.IsSynchronized);
        }
    }
}
