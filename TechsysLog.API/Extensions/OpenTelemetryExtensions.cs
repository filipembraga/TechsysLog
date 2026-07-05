// Arquivo: TechsysLog.API/Extensions/OpenTelemetryExtensions.cs

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TechsysLog.API.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddTechsysLogObservability(
        this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(
                    serviceName: "TechsysLog.API",
                    serviceVersion: "1.0.0"))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = httpContext =>
                    {
                        var path = httpContext.Request.Path.Value ?? string.Empty;
                        return !path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
                            && !path.StartsWith("/scalar", StringComparison.OrdinalIgnoreCase)
                            && !path.StartsWith("/openapi", StringComparison.OrdinalIgnoreCase)
                            && !path.Equals("/favicon.ico", StringComparison.OrdinalIgnoreCase);
                    };
                })
                .AddHttpClientInstrumentation()
                .AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources")
                .AddOtlpExporter());

        return services;
    }
}