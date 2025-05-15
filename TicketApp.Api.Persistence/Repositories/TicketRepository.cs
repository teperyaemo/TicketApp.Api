using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;

namespace TicketApp.Api.Persistence.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketAppDbContext _context;

    public TicketRepository(TicketAppDbContext context)
    {
        _context = context;
    }

    public ValueTask<Ticket?> GetTicketAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Tickets.FindAsync(id, cancellationToken);
    }

    public Task<List<Ticket>> GetUserTicketPageAsync(Guid userId, int skip, int take,
        CancellationToken cancellationToken)
    {
        return _context.Tickets.Where(x=>x.UserId == userId).Include(x=> x.Concert).Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateTicket(Ticket ticket, CancellationToken cancellationToken)
    {
        await _context.Tickets.AddAsync(ticket, cancellationToken);
    }

    public void DeleteTicket(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
    }

    public IQueryable<Ticket> GetQueryable()
    {
        return _context.Tickets.AsQueryable();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}