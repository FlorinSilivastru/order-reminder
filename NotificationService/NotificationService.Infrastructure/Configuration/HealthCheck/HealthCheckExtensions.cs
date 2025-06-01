namespace NotificationService.Infrastructure.Configuration.HealthCheck;

using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

public static class HealthCheckExtensions
{
    public static void MapHealthCheckEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health/ready", new HealthCheckOptions()
        {
            Predicate = (check) => check.Tags.Contains("ready"),

            ResponseWriter = async (context, report) =>
            {
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        error = e.Value.Exception?.Message,
                    }),
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            },
        });

        app.MapHealthChecks("/health/live", new HealthCheckOptions()
        {
            ResponseWriter = async (context, report) =>
            {
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new
                    {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description,
                        error = e.Value.Exception?.Message,
                    }),
                });

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            },
        });
    }
}
