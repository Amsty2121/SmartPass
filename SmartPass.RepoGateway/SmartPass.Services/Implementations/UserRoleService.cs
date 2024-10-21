using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessLevels;
using SmartPass.Services.Models.DTOs.UserRoles;
using System.Data;
using System.Data.SqlTypes;

namespace SmartPass.Services.Implementations
{
    public class UserRoleService(IGenericRepository<SmartPassContext, UserRole> userRoleRepo) : IUserRoleService
    {
        private IGenericRepository<SmartPassContext, UserRole> UserRoleRepo { get; } = userRoleRepo;

        public async Task<Option<UserRoleDto>> Get(Guid id, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetById(id, ct);
            return role is null ? null : new UserRoleDto(role);
        }

        public async Task<IEnumerable<UserRoleDto>> GetAll(CancellationToken ct = default)
        {
            var roles = await UserRoleRepo.GetAll(ct);
            return !roles.Any() ? Enumerable.Empty<UserRoleDto>() : roles.Select(r => new UserRoleDto(r));
        }

        public async Task<Result<UserRoleDto>> Create(AddUserRoleDto addDto, CancellationToken ct = default)
        {
            var roles = await UserRoleRepo.GetWhere(r => r.Name.Equals(addDto.Name), ct);

            if (!roles.Any())
            { 
                return new Result<UserRoleDto>(new DuplicateNameException($"DuplicateException: Insertion failed - Role {addDto.Name} already exists"));
            }

            var role = new UserRole
            {
                Id = Guid.NewGuid(),
                Name = addDto.Name,
                Description = addDto.Description,
                CreateUtcDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            var result = await UserRoleRepo.Add(role, ct);

            return result > 0 ? new UserRoleDto(role) : new Result<UserRoleDto>(new SqlTypeException($"DbException:Insertion failed - Role {addDto.Name}"));
        }

        public async Task<Result<UserRoleDto>> Delete(Guid id, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetById(id, ct);

            if (role is null)
            {
                return new Result<UserRoleDto>(new KeyNotFoundException($"NotFoundException: Deletion failed - Not found Role with id {id}")); ;
            }

            var result = await UserRoleRepo.Remove(role, ct);

            return result > 0 ? new UserRoleDto(role) : new Result<UserRoleDto>(new SqlTypeException($"DbException: Deletion failed - Role with id {id}"));
        }

        public async Task<Result<UserRoleDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetById(id, ct);

            if (role is null)
            {
                return new Result<UserRoleDto>(new KeyNotFoundException($"NotFoundException: Soft Deletion failed - Not found Role with id {id}")); ;
            }

            role.IsDeleted = true;
            role.DeletedUtcDate = role.UpdateUtcDate = DateTime.UtcNow;
            var result = await UserRoleRepo.Update(role, ct);

            return result > 0 ? new UserRoleDto(role) : new Result<UserRoleDto>(new SqlTypeException($"DbException: Soft Deletion failed - Role with id {id}"));
        }

        public async Task<Result<UserRoleDto>> Update(UpdateUserRoleDto updateDto, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetById(updateDto.Id, ct);

            if (role is null)
            {
                return new Result<UserRoleDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found Role with id {updateDto.Id}")); ;
            }
            var toChange = false;

            if (!string.IsNullOrWhiteSpace(updateDto.Name) && !updateDto.Name.Equals(role.Name))
            {
                var roleWithName = (await UserRoleRepo.GetWhere(x => x.Name.Equals(updateDto.Name), ct)).FirstOrDefault();
                if (roleWithName is null)
                {
                    return new Result<UserRoleDto>(new ArgumentException($"DbException: Update failed - The Role with the name {updateDto.Name} already exists"));
                }

                role.Name = updateDto.Name;
                toChange = true;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
            {
                role.Description = updateDto.Description;
                toChange = true;
            }

            if (!toChange) 
            { 
                return new Result<UserRoleDto>(new ArgumentException($"DbException: Update failed - Nothing to update for Role with id {updateDto.Id}"));
            }

            role.UpdateUtcDate = DateTime.UtcNow;
            var result = await UserRoleRepo.Update(role, ct);

            return result > 0 ? new UserRoleDto(role) : new Result<UserRoleDto>(new SqlTypeException($"DbException: Update failed - Role with id {updateDto.Id}"));
        }
    }
}
