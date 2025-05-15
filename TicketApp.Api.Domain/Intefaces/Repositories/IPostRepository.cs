using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Domain.Intefaces.Repositories;

public interface IPostRepository
{
    ValueTask<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Post>> GetPostPageAsync(int skip, int take, CancellationToken cancellationToken);
    Task CreatePost(Post post, CancellationToken cancellationToken);
    void DeletePost(Post post);
    IQueryable<Post> GetQueryable();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}