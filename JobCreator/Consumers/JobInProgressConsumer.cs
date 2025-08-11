namespace JobCreator.Consumers;

using JobCreator.Services;
using MassTransit;
using Shared.Messages;

public class JobInProgressConsumer : IConsumer<JobInProgressEvent>
{
    private readonly IJobService jobService;

    public JobInProgressConsumer(IJobService jobService)
    {
        this.jobService = jobService;
    }

    public async Task Consume(ConsumeContext<JobInProgressEvent> context)
    {
        var message = context.Message;

        await this.jobService.MarkJobAsInProgressAsync(message.JobId);
    }
}