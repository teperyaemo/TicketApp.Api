using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IUserService
{
    public Task<User> GetUserAsync(string username, CancellationToken cancellationToken);
}