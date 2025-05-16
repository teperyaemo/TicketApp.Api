using Microsoft.AspNetCore.Http;

namespace TicketApp.Api.Domain.Dtos;

public class PostDto
{
    public required string Title { get; set; } = "";
    public required string Text { get; set; } = "";
    public IFormFile? Image { get; set; }
}