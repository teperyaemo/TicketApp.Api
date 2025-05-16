using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IFavoriteService
{
    Task<List<Favorite>> GetFavoriteByUserId(CancellationToken cancellationToken);
    Task CreateFavorite(Guid concertId, CancellationToken cancellationToken);
    Task DeleteFavorite(Guid id, CancellationToken cancellationToken);
}