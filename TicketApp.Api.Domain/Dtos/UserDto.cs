namespace TicketApp.Api.Domain.Dtos;

public class UserDto
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Role { get; set; }
}