using Microsoft.Extensions.DependencyInjection;
using Packages.Middlewares.Audit.Interfaces;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.Configuration.Middleware;

public static class RegisterAuditMiddleware
{
    public static IServiceCollection RegisterAuditLog(this IServiceCollection services)
    {
        services.AddTransient<IAuditLogger, AuditLogService>();
        return services;
    }
}
