using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicketApp.Api.Persistence.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<TicketAppDbContext>();

        dbContext.Database.Migrate();
    }
}