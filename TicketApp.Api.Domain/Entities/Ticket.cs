namespace TicketApp.Api.Domain.Entities;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ConcertId { get; set; }

    public virtual User? User { get; set; }
    public virtual Concert? Concert { get; set; }
}