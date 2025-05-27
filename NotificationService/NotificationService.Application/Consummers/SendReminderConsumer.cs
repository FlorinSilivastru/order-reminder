namespace NotificationService.Application.Consummers;

using MassTransit;
using NotificationService.Application.Consummers.Messages;

public class SendReminderConsumer : IConsumer<SendReminderMessage>
{
    public async Task Consume(ConsumeContext<SendReminderMessage> context)
    {
        await Task.FromResult(1);
    }
}