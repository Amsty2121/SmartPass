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

        public async Task<Result<AccessLevelDto>> Create(AddAccessLevelDto addDto, CancellationToken ct = default)
        {
            var accessLevels = await AccessLevelRepo.GetWhere(r => r.Name.Equals(addDto.Name), ct);

            if (!accessLevels.Any())
            {
                return new Result<AccessLevelDto>(new DuplicateNameException($"DuplicateException: Insertion failed - AccessLevel {addDto.Name} already exists"));
            }

            var accessLevel = new AccessLevel
            {
                Id = Guid.NewGuid(),
                Name = addDto.Name,
                Description = addDto.Description,
                CreateUtcDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var result = await AccessLevelRepo.Add(accessLevel, ct);

            return result > 0 ? new AccessLevelDto(accessLevel) : new Result<AccessLevelDto>(new SqlTypeException($"DbException:Insertion failed - AccessLevel {addDto.Name}"));
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

        public async Task<Result<AccessLevelDto>> Update(UpdateAccessLevelDto updateDto, CancellationToken ct = default)
        {
            var accessLevel = await AccessLevelRepo.GetById(updateDto.Id, ct);

            if (accessLevel is null)
            {
                return new Result<AccessLevelDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found AccessLevel with id {updateDto.Id}")); ;
            }

            var toChange = false;

            if (!string.IsNullOrWhiteSpace(updateDto.Name) && !updateDto.Name.Equals(accessLevel.Name))
            {
                var accessLevelWithName = (await AccessLevelRepo.GetWhere(x => x.Name.Equals(updateDto.Name), ct)).FirstOrDefault();
                if (accessLevelWithName is null)
                {
                    return new Result<AccessLevelDto>(new ArgumentException($"DbException: Update failed - The AccessLevel with the name {updateDto.Name} already exists"));
                }
                    
                accessLevel.Name = updateDto.Name;
                toChange = true;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Description))
            {
                accessLevel.Description = updateDto.Description;
                toChange = true;
            }

            if (!toChange)
            {
                return new Result<AccessLevelDto>(new ArgumentException($"DbException: Update failed - Nothing to update for AccessLevel with id {updateDto.Id}"));
            }

            accessLevel.UpdateUtcDate = DateTime.UtcNow;
            var result = await AccessLevelRepo.Update(accessLevel, ct);

            return result > 0 ? new AccessLevelDto(accessLevel) : new Result<AccessLevelDto>(new SqlTypeException($"DbException: Update failed - AccessLevel with id {updateDto.Id}"));
        }
    }
}
