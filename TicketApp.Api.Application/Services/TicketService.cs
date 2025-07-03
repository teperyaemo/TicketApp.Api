using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserContext _userContext;

    public TicketService(ITicketRepository ticketRepository, IUserContext userContext)
    {
        _ticketRepository = ticketRepository;
        _userContext = userContext;
    }

    public async Task<List<Ticket>?> GetUserTicketsAsync(int page, int take,
        CancellationToken cancellationToken)
    {
        return await _ticketRepository.GetUserTicketPageAsync(_userContext.UserId, (page - 1) * take, take, cancellationToken);
    }
}