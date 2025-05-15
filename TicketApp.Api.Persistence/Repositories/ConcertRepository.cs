using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;

namespace TicketApp.Api.Persistence.Repositories;

public class ConcertRepository : IConcertRepository
{
    private readonly TicketAppDbContext _context;

    public ConcertRepository(TicketAppDbContext context)
    {
        _context = context;
    }

    public ValueTask<Concert?> GetConcertAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Concerts.FindAsync(id, cancellationToken);
    }

    public Task<List<Concert>> GetConcertsPageAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return _context.Concerts.Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<List<Concert>> GetConcertsByName(string name, CancellationToken cancellationToken)
    {
        return _context.Concerts.Where(x => x.Name.ToLower().Contains(name)).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateConcertAsync(Concert concert, CancellationToken cancellationToken)
    {
        await _context.Concerts.AddAsync(concert, cancellationToken);
    }

    public void DeleteConcert(Concert concert)
    {
        _context.Concerts.Remove(concert);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}