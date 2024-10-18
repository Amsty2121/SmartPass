using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
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

        public async Task<Result<UserRoleDto>> Create(UserRole entity, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetWhere(r => r.Name.Equals(entity.Name), ct);

            if (role is null || !role.Any())
            { 
                return new Result<UserRoleDto>(new DuplicateNameException($"DuplicateException: Insertion failed - Role {entity.Name} already exists"));
            }

            entity.Id = Guid.NewGuid();
            entity.CreateUtcDate = DateTime.UtcNow;
            entity.IsDeleted = false;
            var result = await UserRoleRepo.Add(entity, ct);

            return result > 0 ? new UserRoleDto(entity) : new Result<UserRoleDto>(new SqlTypeException($"DbException:Insertion failed - Role {entity.Name}"));
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

        public async Task<Result<UserRoleDto>> Update(UserRole entity, CancellationToken ct = default)
        {
            var role = await UserRoleRepo.GetById(entity.Id, ct);

            if (role is null)
            {
                return new Result<UserRoleDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found Role with id {entity.Id}")); ;
            }

            var toChange = false;

            if (entity.Description is not null)
            {
                role.Description = entity.Description;
                role.UpdateUtcDate = DateTime.UtcNow;
                toChange = true;
            }

            if (!toChange) 
            { 
                return new Result<UserRoleDto>(new ArgumentException($"DbException: Update failed - nothing to delete for Role with id {entity.Id}"));
            }

            var result = await UserRoleRepo.Update(role, ct);

            return result > 0 ? new UserRoleDto(role) : new Result<UserRoleDto>(new SqlTypeException($"DbException: Update failed - Role with id {entity.Id}"));
        }
    }
}
