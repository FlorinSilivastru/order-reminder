namespace NotificationService.Application.Consummers;

using MassTransit;
using Messaging.Masstransit.Contracts;

public class SendReminderConsumer : IConsumer<SendReminderMessage>
{
    public async Task Consume(ConsumeContext<SendReminderMessage> context)
    {
        await Task.FromResult(1);
    }
}