using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Repositories;

public interface ITicketRepository
{
    ValueTask<Ticket?> GetTicketAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Ticket>> GetUserTicketPageAsync(Guid userId, int skip, int take, CancellationToken cancellationToken);
    Task CreateTicket(Ticket ticket, CancellationToken cancellationToken);
    void DeleteTicket(Ticket ticket);
    IQueryable<Ticket> GetQueryable();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}