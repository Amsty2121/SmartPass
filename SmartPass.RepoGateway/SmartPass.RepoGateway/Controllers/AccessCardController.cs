﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPass.Services.Interfaces;
using SmartPass.Services.Models.DTOs.AccessCards;
using SmartPass.Services.Models.Requests.AccessCards;

namespace SmartPass.RepoGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessCardController(IAccessCardService accessCardService) : ControllerBase
    {
        private IAccessCardService AccessCardService { get; } = accessCardService;

        private Guid UserId => Guid.Parse(HttpContext.User.Claims.First(t => t.Type == nameof(SmartPass.Repository.Models.Entities.User.Id)).Value);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await AccessCardService.Get(id, ct);
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
                var accessCards = await AccessCardService.GetAll(ct);
                return Ok(accessCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAccessCardRequest addDto, CancellationToken ct = default)
        {
            try
            {
                var result = await AccessCardService.Create(addDto, ct);
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
                var result = await AccessCardService.Delete(id, ct);
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
                var result = await AccessCardService.DeleteSoft(id, ct);
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
        public async Task<IActionResult> Update([FromBody] UpdateAccessCardDto updateDto, CancellationToken ct = default)
        {
            try
            {
                var result = await AccessCardService.Update(updateDto, ct);
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
