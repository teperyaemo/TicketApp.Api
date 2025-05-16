using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;

namespace TicketApp.Api.Persistence.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly TicketAppDbContext _context;

    public FavoriteRepository(TicketAppDbContext context)
    {
        _context = context;
    }

    public ValueTask<Favorite?> GetFavoriteById(Guid id, CancellationToken cancellationToken)
    {
        return _context.Favorites.FindAsync(id, cancellationToken);
    }
    
    public Task<Favorite?> GetFavoriteByConcertId(Guid concertId, CancellationToken cancellationToken)
    {
        return _context.Favorites.FirstOrDefaultAsync(x=> x.ConcertId == concertId, cancellationToken);
    }

    public Task<List<Favorite>> GetFavoriteByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return _context.Favorites.Where(x => x.UserId == userId).Include(x=> x.Concert).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateFavorite(Favorite favorite, CancellationToken cancellationToken)
    {
        await _context.Favorites.AddAsync(favorite, cancellationToken);
    }

    public void DeleteFavorite(Favorite favorite)
    {
        _context.Favorites.Remove(favorite);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}