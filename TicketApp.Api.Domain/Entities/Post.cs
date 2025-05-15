namespace TicketApp.Api.Domain.Entities;

public class Post
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Text { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? EditedAt { get; set; }


    public virtual User? User { get; set; }
}