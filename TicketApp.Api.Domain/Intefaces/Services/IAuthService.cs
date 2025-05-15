namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(string username, string password, CancellationToken cancellationToken);
    Task<string> LoginAsync(string username, string password, CancellationToken cancellationToken);
}