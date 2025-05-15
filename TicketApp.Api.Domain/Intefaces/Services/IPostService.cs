using TicketApp.Api.Domain.Dtos;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Services;

public interface IPostService
{
    Task<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken);
    Task<Post?> UpdatePost(PostUpdateDto postDto, CancellationToken cancellationToken);
    Task<List<Post>?> GetPostPageAsync(int page, int take, CancellationToken cancellationToken);
    Task<Guid> CreatePostAsync(PostDto post, CancellationToken cancellationToken);
    Task DeletePost(Guid id, CancellationToken cancellationToken);
}