using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(IUserRoleService userRoleService) : ControllerBase
    {
        private IUserRoleService UserRoleService { get; } = userRoleService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken ct = default)
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
    }
}
