using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
    IQueryable<User> GetQueryable();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}