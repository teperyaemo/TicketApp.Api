using Microsoft.Extensions.DependencyInjection;
using TicketApp.Api.Application.Services;
using TicketApp.Api.Domain.Intefaces.Services;

namespace TicketApp.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IConcertService, ConcertService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IFavoriteService, FavoriteService>();

        return services;
    }
}