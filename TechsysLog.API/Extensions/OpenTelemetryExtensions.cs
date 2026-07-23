namespace TechsysLog.API.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddTechsysLogObservability(
        this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources"));

        return services;
    }
}