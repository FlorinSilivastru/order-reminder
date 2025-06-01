using Packages.Middlewares.Audit.Contracts;
using Packages.Middlewares.Audit.Interfaces;

namespace CustomerInventoryService.Infrastructure.Services;

public class AuditLogService() : IAuditLogger
{
    public async Task LogAsync(AuditEvent auditEvent)
    {
        await Task.FromResult(0);
    }
}
