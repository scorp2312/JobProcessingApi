using JobProcessor.Services;
using MassTransit;
using Shared.Messages;

namespace JobProcessor.Consumers;

public class JobCreatedConsumer : IConsumer<JobCreatedEvent>
{
    private readonly IJobProcessingService _jobProcessingService;
    
    public JobCreatedConsumer(IJobProcessingService jobProcessingService)
    {
        _jobProcessingService = jobProcessingService;
    }

    public async Task Consume(ConsumeContext<JobCreatedEvent> context)
    {
        var message = context.Message;
        await _jobProcessingService.ProcessJobAsync(message.JobId, message.Description, message.Result);
    }
}