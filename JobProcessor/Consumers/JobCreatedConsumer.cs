namespace JobProcessor.Consumers;

using JobProcessor.Services;
using MassTransit;
using Shared.Messages.Events;

public class JobCreatedConsumer : IConsumer<JobCreatedEvent>
{
    private readonly IJobProcessingService jobProcessingService;

    public JobCreatedConsumer(IJobProcessingService jobProcessingService)
    {
        this.jobProcessingService = jobProcessingService;
    }

    public async Task Consume(ConsumeContext<JobCreatedEvent> context)
    {
        var message = context.Message;
        await this.jobProcessingService.ProcessJobAsync(message.JobId);
    }
}