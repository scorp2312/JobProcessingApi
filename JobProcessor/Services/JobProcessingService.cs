namespace JobProcessor.Services;

using MassTransit;
using Shared.Messages.Events;

public interface IJobProcessingService
{
    Task ProcessJobAsync(Guid jobId);
}

public class JobProcessingService(IPublishEndpoint publishEndpoint) : IJobProcessingService
{
    public async Task ProcessJobAsync(Guid jobId)
    {
        await publishEndpoint.Publish(new JobInProgressEvent
        {
            JobId = jobId,
        });
        await Task.Delay(5000);
        await publishEndpoint.Publish(new JobCompletedEvent
        {
            JobId = jobId,
            CompletedAt = DateTime.UtcNow,
            Result = new Random().Next(1, 101).ToString(),
        });
    }
}