using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Domain.Games;
using Gaming1Challenge.Domain.Players;
using Gaming1Challenge.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming1Challenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IPlayersService, PlayersService>();
        services.AddSingleton<Game>();
        services.AddSingleton<Player>();
        return services;
    }
}
