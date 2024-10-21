using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.Sessions;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(ISessionService sessionService) : ControllerBase
    {
        private ISessionService SessionService { get; } = sessionService;

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await SessionService.Get(id, ct);
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
                var sessions = await SessionService.GetAll(ct);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSessionDto addDto, CancellationToken ct = default)
        {
            try
            {
                var result = await SessionService.Create(addDto, ct);
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
                var result = await SessionService.Delete(id, ct);
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
                var result = await SessionService.DeleteSoft(id, ct);
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
        public async Task<IActionResult> Update([FromBody] UpdateSessionDto updateDto, CancellationToken ct = default)
        {
            try
            {
                var result = await SessionService.Update(updateDto, ct);
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
