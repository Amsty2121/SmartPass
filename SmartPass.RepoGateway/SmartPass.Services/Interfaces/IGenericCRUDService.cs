using LanguageExt;
using LanguageExt.Common;
using SmartPass.Repository.Models.EntityInterfaces;

namespace SmartPass.Services.Interfaces
{
    public interface IGenericCRUDService<TEntity, TId, TDto> where TEntity : class, IBaseEntity
    {
        Task<Option<TDto>> Get(TId id, CancellationToken ct = default);
        Task<IEnumerable<TDto>> GetAll(CancellationToken ct = default);


        Task<Result<TDto>> Create(TEntity entity, CancellationToken ct = default);
        Task<Result<TDto>> Update(TEntity entity, CancellationToken ct = default);
        Task<Result<TDto>> Delete(TId id, CancellationToken ct = default);
        Task<Result<TDto>> DeleteSoft(TId id, CancellationToken ct = default);
    }
}
