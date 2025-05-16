using TicketApp.Api.Domain.Enums;

namespace TicketApp.Api.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public byte[] ProfilePicture { get; set; } = [];
    public string Role { get; set; } = nameof(UserRoles.User);

    public virtual List<Ticket> Tickets { get; set; } = [];
    public virtual List<Post> Posts { get; set; } = [];
    public virtual List<Favorite> Favorites { get; set; } = [];
}