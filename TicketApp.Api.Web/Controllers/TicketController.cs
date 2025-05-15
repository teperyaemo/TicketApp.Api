using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Web.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<List<Ticket>?> Create(int page, int take, CancellationToken cancellationToken)
    {
        var result = await _ticketService.GetUserTicketsAsync(page, take, cancellationToken);

        return result;
    }
}