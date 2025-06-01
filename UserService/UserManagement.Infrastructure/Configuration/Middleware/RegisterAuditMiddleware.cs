namespace UserManagement.Infrastructure.Configuration.Middleware;

using Microsoft.Extensions.DependencyInjection;
using Middlewares.Audit.Interfaces;
using UserManagement.Infrastructure.Services;

public static class RegisterAuditMiddleware
{
    public static IServiceCollection RegisterAuditLog(this IServiceCollection services)
    {
        services.AddTransient<IAuditLogger, AuditLogService>();
        return services;
    }
}
