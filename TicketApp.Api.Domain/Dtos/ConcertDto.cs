using Microsoft.AspNetCore.Http;

namespace TicketApp.Api.Domain.Dtos;

public class ConcertDto
{
    public required string Name { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public int AvailableTicketAmount { get; set; }
    public decimal TicketPrice { get; set; }
    public IFormFile? Image { get; set; }
}