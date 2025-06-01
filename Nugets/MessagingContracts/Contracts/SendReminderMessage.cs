namespace Packages.Messaging.Masstransit.Contracts
{
    public record class SendReminderMessage
    {
        public Guid OrderId { get; init; }

        public DateTime OrderDate { get; init; }

        public string OrderNumber { get; init; }

        public decimal OrderAmount { get; init; }

        public string EmailAddress { get; init; }
    }
}
