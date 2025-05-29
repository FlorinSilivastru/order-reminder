namespace CustomerInventoryService.Infrastructure.Configuration.Middleware;

using CustomerInventoryService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Middlewares.Audit.Interfaces;

public static class RegisterAuditMiddleware
{
    public static void RegisterAuditLog(this IServiceCollection services)
    {
        services.AddTransient<IAuditLogger, AuditLogService>();
    }
}
