using LanguageExt.Pipes;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Users;
using SmartPass.Services.Models.Requests.Users;
using SmartPass.Services.Models.Resposes;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private IUserService UserService { get; } = userService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.Get(id, ct);
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

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var users = await UserService.GetAll(ct);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserDto addDto, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.Create(addDto, ct);
                return result.Match<IActionResult>(
                    Succ: value => Ok(value),
                    Fail: ex => BadRequest(ex.Message)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.Delete(id, ct);
                return result.Match<IActionResult>(
                    Succ: value => Ok(value),
                    Fail: ex => BadRequest(ex.Message)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> DeleteSoft([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.DeleteSoft(id, ct);
                return result.Match<IActionResult>(
                    Succ: value => Ok(value),
                    Fail: ex => BadRequest(ex.Message)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateDto, CancellationToken ct = default)
        {
            try
            {
                var result = await UserService.Update(updateDto, ct);
                return result.Match<IActionResult>(
                    Succ: value => Ok(value),
                    Fail: ex => BadRequest(ex.Message)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Mobile/Login")]
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

        [HttpPost("Mobile/RefreshToken")]
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

    }
}
