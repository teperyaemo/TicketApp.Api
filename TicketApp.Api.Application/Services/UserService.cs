using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public UserService(IUserRepository userRepository, IUserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<User?> GetUserAsync(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserAsync(username, cancellationToken);
    }

    public async Task<User?> GetUserMeAsync(CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(_userContext.UserId, cancellationToken);
    }


    public async Task<User?> UpdateUserProfilePicturAsync(UserUpdateProfilePictureDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(_userContext.UserId, cancellationToken);

        if (user != null)
        {
            user.ProfilePicture = dto.ProfilePicture;
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        return user;
    }
}