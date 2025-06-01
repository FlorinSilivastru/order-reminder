using Packages.Middlewares.Audit.Contracts;

namespace Packages.Middlewares.Audit.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(AuditEvent auditEvent);
}