namespace Packages.Middlewares.Audit.Interfaces;

using Middlewares.Audit.Contracts;

public interface IAuditLogger
{
    Task LogAsync(AuditEvent auditEvent);
}