namespace Packages.Middlewares.Audit.Contracts
{
    public class AuditEvent
    {
        public Guid AuditID { get; set; }

        public string Url { get; set; }

        public string? IpAddress { get; set; }

        public string? UserName { get; set; }

        public string? Request { get; set; }

        public string? Response { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? CorrelationId { get; set; }
    }
}