namespace TicketApp.Api.Domain.Entities;

public class Concert
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public int AvailableTicketAmount { get; set; }
    public int SoldTickets { get; set; }
    public decimal TicketPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public virtual List<Ticket>? Tickets { get; set; } = [];
}