using Gaming1Challenge.Application.Interfaces;
using Gaming1Challenge.Application.Interfaces.Repositories;
using Gaming1Challenge.Application.Interfaces.Repositories.Data;
using Gaming1Challenge.Domain.Games;
using Gaming1Challenge.Domain.Players;
using Gaming1Challenge.Infrastructure.Data;
using Gaming1Challenge.Infrastructure.Data.UnitOfWork;
using Gaming1Challenge.Infrastructure.Repositories;
using Gaming1Challenge.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming1Challenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IGamesService, GamesService>();
        services.AddScoped<IPlayersService, PlayersService>();
        services.AddScoped<Game>();
        services.AddSingleton<Player>();

        services.AddTransient<DbSession>(_ => new DbSession(configuration.GetConnectionString("Gaming1ChallengeDb")!));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGamesRepository, GamesRepository>();

        return services;
    }
}
