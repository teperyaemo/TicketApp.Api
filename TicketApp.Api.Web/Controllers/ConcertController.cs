using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Web.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
public class ConcertController : ControllerBase
{
    private readonly IConcertService _concertService;

    public ConcertController(IConcertService concertService)
    {
        _concertService = concertService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Guid> Create([FromForm] ConcertDto dto, CancellationToken cancellationToken)
    {
        var result = await _concertService.CreateConcert(dto, cancellationToken);

        return result;
    }

    [HttpPost("buyTicket/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Guid> BuyTicket(Guid id, CancellationToken cancellationToken)
    {
        var result = await _concertService.BuyTicketAsync(id, cancellationToken);

        return result;
    }

    [HttpGet("id/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Concert?> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _concertService.GetConcertAsync(id, cancellationToken);

        return result;
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<List<Concert>?> Get(string name, CancellationToken cancellationToken)
    {
        var result = await _concertService.GetConcertsByName(name, cancellationToken);

        return result;
    }

    [HttpGet("paged")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<List<Concert>?> Get(int page, int take, string? name, CancellationToken cancellationToken)
    {
        var result = await _concertService.GetConcertsPageAsync(page, take, name, cancellationToken);

        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<Concert?> Update(Guid id, [FromForm] ConcertDto dto, CancellationToken cancellationToken)
    {
        var result = await _concertService.UpdateConcertAsync(id, dto, cancellationToken);

        return result;
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        await _concertService.DeleteConcert(id, cancellationToken);
    }
}