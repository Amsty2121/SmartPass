using LanguageExt;
using LanguageExt.Common;
using OneOf;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Models.DTOs.Users;
using SmartPass.Services.Models.Requests.Users;
using SmartPass.Services.Models.Resposes;

namespace SmartPass.Services.Interfaces
{
    public interface IUserService
    {
        Task<Option<UserDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<UserDto>> GetAll(CancellationToken ct = default);


        Task<Result<UserDto>> Create(AddUserDto addDto, CancellationToken ct = default);
        Task<Result<UserDto>> Update(UpdateUserDto updateDto, CancellationToken ct = default);
        Task<Result<UserDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<UserDto>> DeleteSoft(Guid id, CancellationToken ct = default);


        Task<Option<UserLoginResponse>> Login(UserLoginRequest request, CancellationToken ct = default);
        Task<Option<UserLoginResponse>> RefreshToken(string refreshToken, CancellationToken ct = default);

        Task<ICollection<UserDto>> GetUsersByRole(RoleValue role, CancellationToken ct = default);
        Task<Option<UserWithCardsDto>> GetUserWithCards(Guid id, CancellationToken ct = default);
        Task<Option<UserWithSessionDto>> GetUserWithSession(Guid id, CancellationToken ct = default);
        Task<IEnumerable<UserWithDeletedFlagDto>> GetUsersWithDeletedFlag(CancellationToken ct = default);
    }
}
