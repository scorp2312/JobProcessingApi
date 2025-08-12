namespace JobCreator.Consumers;

using JobCreator.Services;
using MassTransit;
using Shared.Messages.Events;

public class JobInProgressConsumer(IJobService jobService) : IConsumer<JobInProgressEvent>
{
    public async Task Consume(ConsumeContext<JobInProgressEvent> context)
    {
        var message = context.Message;

        await jobService.MarkJobAsInProgressAsync(message.JobId);
    }
}