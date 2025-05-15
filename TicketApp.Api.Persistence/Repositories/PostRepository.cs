using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;

namespace TicketApp.Api.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly TicketAppDbContext _context;

    public PostRepository(TicketAppDbContext context)
    {
        _context = context;
    }

    public ValueTask<Post?> GetPostAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Posts.FindAsync(id, cancellationToken);
    }

    public Task<List<Post>> GetPostPageAsync(int skip, int take, CancellationToken cancellationToken)
    {
        var result = _context.Posts.Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);
        
        return result;
    }

    public async Task CreatePost(Post post, CancellationToken cancellationToken)
    {
        await _context.Posts.AddAsync(post, cancellationToken);
    }

    public void DeletePost(Post post)
    {
        _context.Remove(post);
    }

    public IQueryable<Post> GetQueryable()
    {
        return _context.Posts.AsQueryable();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}