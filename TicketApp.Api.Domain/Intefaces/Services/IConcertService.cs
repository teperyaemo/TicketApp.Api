using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IConcertService
{
    public Task<Guid> BuyTicketAsync(Guid id, CancellationToken cancellationToken);
    Task<Concert?> GetConcertAsync(Guid id, CancellationToken cancellationToken);
    Task<Concert?> UpdateConcertAsync(Guid id, ConcertDto concertDto, CancellationToken cancellationToken);
    Task<List<Concert>?> GetConcertsPageAsync(int page, int take, CancellationToken cancellationToken);
    Task<List<Concert>?> GetConcertsByName(string name, CancellationToken cancellationToken);
    Task<Guid> CreateConcert(ConcertDto concertDto, CancellationToken cancellationToken);
    Task DeleteConcert(Guid id, CancellationToken cancellationToken);
}