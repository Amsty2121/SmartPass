using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Zones;
using System.Data.SqlTypes;
using System.Data;

namespace SmartPass.Services.Implementations
{
    public class ZoneService(IGenericRepository<SmartPassContext, Zone> zoneRepo) : IZoneService
    {
        public IGenericRepository<SmartPassContext, Zone> ZoneRepo { get; } = zoneRepo;

        public async Task<Result<ZoneDto>> Create(Zone entity, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetWhere(r => r.Name.Equals(entity.Name), ct);

            if (zone is null || !zone.Any())
            {
                return new Result<ZoneDto>(new DuplicateNameException($"DuplicateException: Insertion failed - Zone {entity.Name} already exists"));
            }

            entity.Id = Guid.NewGuid();
            entity.CreateUtcDate = DateTime.UtcNow;
            entity.IsDeleted = false;
            var result = await ZoneRepo.Add(entity, ct);

            return result > 0 ? new ZoneDto(entity) : new Result<ZoneDto>(new SqlTypeException($"DbException:Insertion failed - Zone {entity.Name}"));
        }

        public async Task<Result<ZoneDto>> Delete(Guid id, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetById(id, ct);

            if (zone is null)
            {
                return new Result<ZoneDto>(new KeyNotFoundException($"NotFoundException: Deletion failed - Not found Zone with id {id}")); ;
            }

            var result = await ZoneRepo.Remove(zone, ct);

            return result > 0 ? new ZoneDto(zone) : new Result<ZoneDto>(new SqlTypeException($"DbException: Deletion failed - Zone with id {id}"));
        }

        public async Task<Result<ZoneDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetById(id, ct);

            if (zone is null)
            {
                return new Result<ZoneDto>(new KeyNotFoundException($"NotFoundException: Soft Deletion failed - Not found Zone with id {id}")); ;
            }

            zone.IsDeleted = true;
            zone.DeletedUtcDate = zone.UpdateUtcDate = DateTime.UtcNow;
            var result = await ZoneRepo.Update(zone, ct);

            return result > 0 ? new ZoneDto(zone) : new Result<ZoneDto>(new SqlTypeException($"DbException: Soft Deletion failed - Zone with id {id}"));
        }

        public async Task<Option<ZoneDto>> Get(Guid id, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetById(id, ct);
            return zone is null ? null : new ZoneDto(zone);
        }

        public async Task<IEnumerable<ZoneDto>> GetAll(CancellationToken ct = default)
        {
            var accessCards = await ZoneRepo.GetAll(ct);
            return !accessCards.Any() ? Enumerable.Empty<ZoneDto>() : accessCards.Select(ac => new ZoneDto(ac));
        }

        public async Task<Result<ZoneDto>> Update(Zone entity, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetById(entity.Id, ct);

            if (zone is null)
            {
                return new Result<ZoneDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found Zone with id {entity.Id}")); ;
            }

            var toChange = false;

            if (entity.Description is not null)
            {
                zone.Description = entity.Description;
                zone.UpdateUtcDate = DateTime.UtcNow;
                toChange = true;
            }

            if (!toChange)
            {
                return new Result<ZoneDto>(new ArgumentException($"DbException: Update failed - nothing to update for Zone with id {entity.Id}"));
            }

            var result = await ZoneRepo.Update(zone, ct);

            return result > 0 ? new ZoneDto(zone) : new Result<ZoneDto>(new SqlTypeException($"DbException: Update failed - Zone with id {entity.Id}"));

        }
    }
}
