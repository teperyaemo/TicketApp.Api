using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Repositories;

public interface IConcertRepository
{
    ValueTask<Concert?> GetConcertAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Concert>> GetConcertsPageAsync(int skip, int take, CancellationToken cancellationToken);
    Task<List<Concert>> GetConcertsByName(string name, CancellationToken cancellationToken);
    Task CreateConcertAsync(Concert concert, CancellationToken cancellationToken);
    void DeleteConcert(Concert concert);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}