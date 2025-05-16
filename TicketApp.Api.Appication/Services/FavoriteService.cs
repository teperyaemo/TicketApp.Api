using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Appication.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IUserContext _userContext;

    public FavoriteService(IFavoriteRepository favoriteRepository, IUserContext userContext)
    {
        _favoriteRepository = favoriteRepository;
        _userContext = userContext;
    }

    public Task<List<Favorite>> GetFavoriteByUserId(CancellationToken cancellationToken)
    {
        return _favoriteRepository.GetFavoriteByUserId(_userContext.UserId, cancellationToken);
    }

    public async Task CreateFavorite(Guid concertId, CancellationToken cancellationToken)
    {
        var favorite = new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = _userContext.UserId,
            ConcertId = concertId,
            User = null,
            Concert = null
        };

        await _favoriteRepository.CreateFavorite(favorite, cancellationToken);
        await _favoriteRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteFavorite(Guid concertId, CancellationToken cancellationToken)
    {
        var favorite = await _favoriteRepository.GetFavoriteByConcertId(concertId, cancellationToken);

        if (favorite != null && favorite.UserId == _userContext.UserId)
        {
            _favoriteRepository.DeleteFavorite(favorite);
            await _favoriteRepository.SaveChangesAsync(cancellationToken);
        }
    }
}