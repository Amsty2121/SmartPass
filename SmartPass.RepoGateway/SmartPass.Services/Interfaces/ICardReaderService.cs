using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.CardReaders;

namespace SmartPass.Services.Interfaces
{
    public interface ICardReaderService
    {
        Task<Option<CardReaderDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<CardReaderDto>> GetAll(CancellationToken ct = default);


        Task<Result<CardReaderDto>> Create(AddCardReaderDto addDto, CancellationToken ct = default);
        Task<Result<CardReaderDto>> Update(UpdateCardReaderDto updateDto, CancellationToken ct = default);
        Task<Result<CardReaderDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<CardReaderDto>> DeleteSoft(Guid id, CancellationToken ct = default);


        //Task<Option<CardReaderWithSession>> GetDeviceWithSessions(Guid id, CancellationToken ct = default);
        //Task<IEnumerable<CardReaderWithDeletedFlagDto>> GetDevicesWithDeletedFlag(CancellationToken ct = default);
    }
}
