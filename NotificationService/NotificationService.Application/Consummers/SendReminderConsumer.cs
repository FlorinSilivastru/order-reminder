namespace NotificationService.Application.Consummers;

using MassTransit;
using MessagingContracts;

public class SendReminderConsumer : IConsumer<SendReminderMessage>
{
    public async Task Consume(ConsumeContext<SendReminderMessage> context)
    {
        await Task.FromResult(1);
    }
}