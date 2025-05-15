using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TicketApp.Api.Domain.Entities;

namespace TicketApp.Api.Persistence;

public class TicketAppDbContext : DbContext
{
    private bool _disposed;

    public TicketAppDbContext(DbContextOptions<TicketAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) Dispose();
            _disposed = true;
        }
    }

    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}