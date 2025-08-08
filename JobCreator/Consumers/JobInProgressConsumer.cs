using JobCreator.Services;
using MassTransit;
using Shared.Messages;

namespace JobCreator.Consumers;

public class JobInProgressConsumer : IConsumer<JobInProgressEvent>
{
    private readonly IJobService _jobService;

    public JobInProgressConsumer(IJobService jobService)
    {
        _jobService = jobService;
    }

    public async Task Consume(ConsumeContext<JobInProgressEvent> context)
    {
        var message = context.Message;
        
        await _jobService.MarkJobAsInProgressAsync(message.JobId);
    }
}