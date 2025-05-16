using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;
using TicketApp.Api.Domain.Intefaces.Repositories;

namespace TicketApp.Api.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TicketAppDbContext _context;

    public UserRepository(TicketAppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetUserAsync(string username, CancellationToken cancellationToken)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
    }
    
    public ValueTask<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Users.FindAsync(id, cancellationToken);
    }

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public IQueryable<User> GetQueryable()
    {
        return _context.Users.AsQueryable();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}