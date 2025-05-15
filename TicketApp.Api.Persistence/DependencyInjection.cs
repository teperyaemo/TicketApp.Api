using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketApp.Api.Domain.Intefaces.Repositories;
using TicketApp.Api.Persistence.Repositories;

namespace TicketApp.Api.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddDbContext<TicketAppDbContext>(options => { options.UseNpgsql(connectionString); });

        services.AddScoped<IConcertRepository, ConcertRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}