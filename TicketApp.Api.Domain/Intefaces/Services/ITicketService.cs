using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface ITicketService
{
    public Task<List<Ticket>?>
        GetUserTicketsAsync(int page, int take, CancellationToken cancellationToken);
}