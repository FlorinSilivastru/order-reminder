namespace Packages.Middlewares.Audit.Interfaces;

using Packages.Middlewares.Audit.Contracts;

public interface IAuditLogger
{
    Task LogAsync(AuditEvent auditEvent);
}