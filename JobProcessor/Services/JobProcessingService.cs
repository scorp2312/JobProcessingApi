using MassTransit;
using Shared.Messages;

namespace JobProcessor.Services;

public interface IJobProcessingService
{
    Task ProcessJobAsync(Guid jobId, string? description);
}

public class JobProcessingService : IJobProcessingService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public JobProcessingService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task ProcessJobAsync(Guid jobId, string? description)
    {
        
        
        await Task.Delay(5000);
        
        
        await _publishEndpoint.Publish(new JobCompletedEvent
        {
            JobId = jobId,
            CompletedAt = DateTime.UtcNow
        });
        
    }
}