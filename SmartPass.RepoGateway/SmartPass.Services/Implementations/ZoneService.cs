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

        public async Task<Result<ZoneDto>> Create(AddZoneDto addDto, CancellationToken ct = default)
        {
            var zones = await ZoneRepo.GetWhere(r => r.Name.Equals(addDto.Name), ct);

            if (zones is null || !zones.Any())
            {
                return new Result<ZoneDto>(new DuplicateNameException($"DuplicateException: Insertion failed - Zone {addDto.Name} already exists"));
            }

            var zone = new Zone
            {
                Id = Guid.NewGuid(),
                Name = addDto.Name,
                Description = addDto.Description,
                //AccessLevelId = addDto.AccessLevelId,
                CreateUtcDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var result = await ZoneRepo.Add(zone, ct);

            return result > 0 ? new ZoneDto(zone) : new Result<ZoneDto>(new SqlTypeException($"DbException:Insertion failed - Zone {addDto.Name}"));
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

        public async Task<Result<ZoneDto>> Update(UpdateZoneDto updateDto, CancellationToken ct = default)
        {
            var zone = await ZoneRepo.GetById(updateDto.Id, ct);

            if (zone is null)
            {
                return new Result<ZoneDto>(new KeyNotFoundException($"NotFoundException: Update failed - Not found Zone with id {updateDto.Id}")); ;
            }

            var toChange = false;

            if (updateDto.Description is not null)
            {
                zone.Description = updateDto.Description;
                zone.UpdateUtcDate = DateTime.UtcNow;
                toChange = true;
            }

            if (!toChange)
            {
                return new Result<ZoneDto>(new ArgumentException($"DbException: Update failed - nothing to update for Zone with id {updateDto.Id}"));
            }

            var result = await ZoneRepo.Update(zone, ct);

            return result > 0 ? new ZoneDto(zone) : new Result<ZoneDto>(new SqlTypeException($"DbException: Update failed - Zone with id {updateDto.Id}"));

        }
    }
}
