using MassTransit;
using Packages.Messaging.Masstransit.Contracts;

namespace NotificationService.Application.Consummers;

public class SendReminderConsumer : IConsumer<SendReminderMessage>
{
    public async Task Consume(ConsumeContext<SendReminderMessage> context)
    {
        await Task.FromResult(1);
    }
}