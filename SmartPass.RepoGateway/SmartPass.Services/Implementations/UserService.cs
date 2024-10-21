using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Users;
using SmartPass.Services.Utility;
using System.Data;
using System.Data.SqlTypes;

namespace SmartPass.Services.Implementations
{
    public class UserService(IGenericRepository<SmartPassContext, User> userRepository) : IUserService
    {
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

        public async Task<Result<UserDto>> Create(AddUserDto addDto, CancellationToken ct)
        {
            var users = await UserRepository.GetWhere(r => r.UserName.Equals(addDto.UserName), ct);

            if (!users.Any())
            {
                return new Result<UserDto>(new DuplicateNameException($"DuplicateException: Insertion failed - User {addDto.UserName} already exists"));
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = addDto.UserName,
                Department = addDto.Department,
                Description = addDto.Description,
                CreateUtcDate = DateTime.UtcNow,
                IsDeleted = false,
                Password = PasswordUtility.HashPassword(addDto.Password)
            };
            var result = await UserRepository.Add(user, ct);

            return result > 0 ? new UserDto(user) : new Result<UserDto>(new SqlTypeException($"DbException:Insertion failed - User {addDto.UserName}"));
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


    }
}
