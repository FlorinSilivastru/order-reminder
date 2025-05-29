namespace UserManagement.Infrastructure.Configuration.Middleware;

using Microsoft.Extensions.DependencyInjection;
using Middlewares.Audit.Interfaces;
using UserMangement.Infrastructure.Services;

public static class RegisterAuditMiddleware
{
    public static void RegisterAuditLog(this IServiceCollection services)
    {
        services.AddTransient<IAuditLogger, AuditLogService>();
    }
}
