namespace JobCreator.Consumers;

using JobCreator.Services;
using MassTransit;
using Shared.Messages.Events;

public class JobCompletedConsumer : IConsumer<JobCompletedEvent>
{
    private readonly IJobService jobService;

    public JobCompletedConsumer(IJobService jobService)
    {
        this.jobService = jobService;
    }

    public async Task Consume(ConsumeContext<JobCompletedEvent> context)
    {
        var message = context.Message;

        await this.jobService.MarkJobAsCompletedAsync(message.JobId, message.CompletedAt, message.Result);
    }
}