namespace JobCreator.Consumers;

using JobCreator.Services;
using MassTransit;
using Shared.Messages.Events;

public class JobInProgressConsumer(JobService jobService) : IConsumer<JobInProgressEvent>
{
    public async Task Consume(ConsumeContext<JobInProgressEvent> context)
    {
        var message = context.Message;

        await jobService.MarkJobAsInProgressAsync(message.JobId);
    }
}