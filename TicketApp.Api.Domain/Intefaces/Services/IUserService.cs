using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IUserService
{
    Task<User?> GetUserAsync(string username, CancellationToken cancellationToken);
    Task<User?> GetUserMeAsync(CancellationToken cancellationToken);
    Task<User?> UpdateUserProfilePicturAsync(UserUpdateProfilePictureDto dto, CancellationToken cancellationToken);
}