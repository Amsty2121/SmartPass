using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessLevels;
using System.Data.SqlTypes;
using System.Data;

namespace SmartPass.Services.Implementations
{
    public class AccessLevelService(IGenericRepository<SmartPassContext, AccessLevel> accessLevelRepo) : IAccessLevelService
    {
        public IGenericRepository<SmartPassContext, AccessLevel> AccessLevelRepo { get; } = accessLevelRepo;

        public async Task<Option<AccessLevelDto>> Get(Guid id, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetById(id, ct);
            return accessLevel is null ? null : new AccessLevelDto(accessLevel);
        }

        public async Task<IEnumerable<AccessLevelDto>> GetAll(CancellationToken ct = default)
        {
            var accessLevels = await AccessLevelRepo.GetAll(ct);
            return !accessLevels.Any() ? Enumerable.Empty<AccessLevelDto>() : accessLevels.Select(r => new AccessLevelDto(r));
        }

        public async Task<Result<AccessLevelDto>> Create(AccessLevel entity, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetWhere(r => r.Name.Equals(entity.Name), ct);

            if (accessLevel is null || !accessLevel.Any())
            {
                return new Result<AccessLevelDto>(new DuplicateNameException($"DuplicateException: Insertion failed - AccessLevel {entity.Name} already exists"));
            }

            entity.Id = Guid.NewGuid();
            entity.CreateUtcDate = DateTime.UtcNow;
            entity.IsDeleted = false;
            var result = await AccessLevelRepo.Add(entity, ct);

            return result > 0 ? new AccessLevelDto(entity) : new Result<AccessLevelDto>(new SqlTypeException($"DbException:Insertion failed - AccessLevel {entity.Name}"));
        }

        public async Task<Result<AccessLevelDto>> Delete(Guid id, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetById(id, ct);

            if (accessLevel is null)
            {
                return new Result<AccessLevelDto>(new KeyNotFoundException($"NotFoundException: Deletion failed - Not found AccessLevel with id {id}")); ;
            }

            var result = await AccessLevelRepo.Remove(accessLevel, ct);

            return result > 0 ? new AccessLevelDto(accessLevel) : new Result<AccessLevelDto>(new SqlTypeException($"DbException: Deletion failed - AccessLevel with id {id}"));
        }

        public async Task<Result<AccessLevelDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetById(id, ct);

            if (accessLevel is null)
            {
                return new Result<AccessLevelDto>(new KeyNotFoundException($"NotFoundException: Soft Deletion failed - Not found AccessLevel with id {id}")); ;
            }

            accessLevel.IsDeleted = true;
            accessLevel.DeletedUtcDate = accessLevel.UpdateUtcDate = DateTime.UtcNow;
            var result = await AccessLevelRepo.Update(accessLevel, ct);

            return result > 0 ? new AccessLevelDto(accessLevel) : new Result<AccessLevelDto>(new SqlTypeException($"DbException: Soft Deletion failed - AccessLevel with id {id}"));
        }

        public async Task<Result<AccessLevelDto>> Update(AccessLevel entity, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetById(entity.Id, ct);

            if (accessLevel is null)
            {
                return new Result<AccessLevelDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found AccessLevel with id {entity.Id}")); ;
            }

            var toChange = false;

            if (entity.Description is not null)
            {
                accessLevel.Description = entity.Description;
                toChange = true;
            }

            if (!toChange)
            {
                return new Result<AccessLevelDto>(new ArgumentException($"DbException: Update failed - nothing to update for AccessLevel with id {entity.Id}"));
            }

            accessLevel.UpdateUtcDate = DateTime.UtcNow;
            var result = await AccessLevelRepo.Update(accessLevel, ct);

            return result > 0 ? new AccessLevelDto(accessLevel) : new Result<AccessLevelDto>(new SqlTypeException($"DbException: Update failed - AccessLevel with id {entity.Id}"));
        }
    }
}
