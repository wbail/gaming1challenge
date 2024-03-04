using Gaming1Challenge.Api.Configurations;
using Gaming1Challenge.Application;
using Gaming1Challenge.Infrastructure;

namespace Gaming1Challenge.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApplication();
        services.AddInfrastructure();
        services.AddHealthChecks();
        
        services.AddApiVersioningConfiguration();

        return services;
    }
}
