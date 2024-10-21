using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.Zones;

namespace SmartPass.Services.Interfaces
{
    public interface IZoneService
    {
        Task<Option<ZoneDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<ZoneDto>> GetAll(CancellationToken ct = default);


        Task<Result<ZoneDto>> Create(AddZoneDto addDto, CancellationToken ct = default);
        Task<Result<ZoneDto>> Update(UpdateZoneDto updateDto, CancellationToken ct = default);
        Task<Result<ZoneDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<ZoneDto>> DeleteSoft(Guid id, CancellationToken ct = default);
    }
}
