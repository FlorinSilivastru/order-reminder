namespace UserService.Infrastructure.Services;

using Packages.Middlewares.Audit.Contracts;
using Packages.Middlewares.Audit.Interfaces;

public class AuditLogService() : IAuditLogger
{
    public async Task LogAsync(AuditEvent auditEvent)
    {
        await Task.FromResult(0);
    }
}
