using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.UserRoles;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(IUserRoleService userRoleService) : ControllerBase
    {
        private IUserRoleService UserRoleService { get; } = userRoleService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await UserRoleService.Get(id, ct);

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
                var roles = await UserRoleService.GetAll(ct);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserRoleDto addDto, CancellationToken ct = default)
        {
            try
            {
                var result = await UserRoleService.Create(addDto, ct);
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
                var result = await UserRoleService.Delete(id, ct);
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
                var result = await UserRoleService.DeleteSoft(id, ct);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRoleDto updateDto, CancellationToken ct = default)
        {
            try
            {
                var result = await UserRoleService.Update(updateDto, ct);
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
    }
}
