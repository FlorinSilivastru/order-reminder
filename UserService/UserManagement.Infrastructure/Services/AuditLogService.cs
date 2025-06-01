namespace UserManagement.Infrastructure.Services;

using Middlewares.Audit.Contracts;
using Middlewares.Audit.Interfaces;

public class AuditLogService() : IAuditLogger
{
    public async Task LogAsync(AuditEvent auditEvent)
    {
        await Task.FromResult(0);
    }
}
