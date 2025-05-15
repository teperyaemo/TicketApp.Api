using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Appication.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserAsync(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserAsync(username, cancellationToken);
    }
}