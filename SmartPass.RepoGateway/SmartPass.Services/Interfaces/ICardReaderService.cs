using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Models.DTOs.Devices;

namespace SmartPass.Services.Interfaces
{
    public interface ICardReaderService: IGenericCRUDService<CardReader, Guid, CardReaderDto>
    {
        Task<Option<CardReaderWithSession>> GetDeviceWithSessions(Guid id, CancellationToken ct = default);
        Task<IEnumerable<CardReaderWithDeletedFlagDto>> GetDevicesWithDeletedFlag(CancellationToken ct = default);
    }
}
