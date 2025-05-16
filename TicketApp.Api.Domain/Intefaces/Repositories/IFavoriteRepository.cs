using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Repositories;

public interface IFavoriteRepository
{
    ValueTask<Favorite?> GetFavoriteById(Guid id, CancellationToken cancellationToken);
    Task<List<Favorite>> GetFavoriteByUserId(Guid userId, CancellationToken cancellationToken);
    Task<Favorite?> GetFavoriteByConcertId(Guid concertId, CancellationToken cancellationToken);
    Task CreateFavorite(Favorite favorite, CancellationToken cancellationToken);
    void DeleteFavorite(Favorite favorite);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}