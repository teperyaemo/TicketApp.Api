namespace TicketApp.Api.Domain.Dtos;

public class ConcertUpdateDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public int AvailableTicketAmount { get; set; }
}