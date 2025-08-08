using JobCreator.Services;
using MassTransit;
using Shared.Messages;

namespace JobCreator.Consumers;

public class JobCompletedConsumer : IConsumer<JobCompletedEvent>
{
    private readonly IJobService _jobService;

    public JobCompletedConsumer(IJobService jobService)
    {
        _jobService = jobService;
    }
    public async Task Consume(ConsumeContext<JobCompletedEvent> context)
    {
        var message = context.Message;
        
        
        await _jobService.MarkJobAsCompletedAsync(message.JobId, message.CompletedAt, message.Result);
        
    }
}