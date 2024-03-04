using Asp.Versioning;

namespace Gaming1Challenge.Api.Configurations;

public static class ApiVersioningConfiguration
{
    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader()
                // , new HeaderApiVersionReader("X-Api-Version")
                );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
