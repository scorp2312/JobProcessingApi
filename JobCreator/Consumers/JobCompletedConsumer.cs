namespace JobCreator.Consumers;

using JobCreator.Services;
using MassTransit;
using Shared.Messages.Events;

public class JobCompletedConsumer(IJobService jobService) : IConsumer<JobCompletedEvent>
{
    public async Task Consume(ConsumeContext<JobCompletedEvent> context)
    {
        var message = context.Message;

        await jobService.MarkJobAsCompletedAsync(message.JobId, message.CompletedAt, message.Result);
    }
}