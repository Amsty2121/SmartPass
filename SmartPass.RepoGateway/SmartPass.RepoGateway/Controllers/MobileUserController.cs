using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Implementations;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.Requests.Users;

namespace SmartPass.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileUserController(IUserService userService, IAccessCardService accessCardService) : ControllerBase
    {
        public IUserService UserService { get; } = userService;
        public IAccessCardService AccessCardService { get; } = accessCardService;
        private Guid UserId => Guid.Parse(HttpContext.User.Claims.First(t => t.Type == nameof(SmartPass.Repository.Models.Entities.User.Id)).Value);


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.Login(request, ct);
                return result.Match<IActionResult>(
                    Some: value => Ok(value),
                    None: () => NotFound()
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.RefreshToken(token, ct);

                return result.Match<IActionResult>(
                    Some: value => Ok(value),
                    None: () => NotFound()
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("MyCards")]
        public async Task<IActionResult> GetAccessCardsByUserId(CancellationToken ct = default)
        {
            try
            {
                var result = await AccessCardService.GetAllByUserIdMobile(UserId, ct);
                return result.Match<IActionResult>(
                    Some: value => Ok(value),
                    None: () => NotFound()
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[Authorize]
        [HttpGet("Synchronized")]
        public async Task<IActionResult> IsUserSynchronized(CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.IsUserSynchronized(UserId, ct);
                return result.Match<IActionResult>(
                    Some: value => Ok(value),
                    None: () => NotFound()
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [Authorize]
        [HttpGet("TokenVerify")]
        public IActionResult TokenVerify(CancellationToken ct = default)
        {
            return Ok();
        }
    }
}
