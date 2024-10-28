using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Contexts;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.Entities;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.CardReaders;

namespace SmartPass.Services.Implementations
{
    public class CardReaderService(IGenericRepository<SmartPassContext, CardReader> cardReaderRepo) : ICardReaderService
    {
        public IGenericRepository<SmartPassContext, CardReader> CardReaderRepo { get; } = cardReaderRepo;

        public Task<Result<CardReaderDto>> Create(AddCardReaderDto addDto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardReaderDto>> Delete(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardReaderDto>> DeleteSoft(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Option<CardReaderDto>> Get(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CardReaderDto>> GetAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CardReaderDto>> Update(UpdateCardReaderDto updateDto, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
