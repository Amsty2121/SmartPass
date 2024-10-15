using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardReaderController(ICardReaderService cardReaderService) : ControllerBase
    {
        public ICardReaderService CardReaderService { get; } = cardReaderService;

        /*[HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await CardReaderService.Get(id, ct);
                return result.Match<IActionResult>(
                        value => Ok(value),
                        findFailed => NotFound());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
