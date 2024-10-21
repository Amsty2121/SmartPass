using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.UserRoles;

namespace SmartPass.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<Option<UserRoleDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<UserRoleDto>> GetAll(CancellationToken ct = default);


        Task<Result<UserRoleDto>> Create(AddUserRoleDto addDto, CancellationToken ct = default);
        Task<Result<UserRoleDto>> Update(UpdateUserRoleDto updateDto, CancellationToken ct = default);
        Task<Result<UserRoleDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<UserRoleDto>> DeleteSoft(Guid id, CancellationToken ct = default);
    }
}
