namespace JobProcessor.Services;

using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Shared.Messages;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileNameMustMatchTypeName", Justification = "Reviewed.")]
public interface IJobProcessingService
{
    Task ProcessJobAsync(Guid jobId);
}

public class JobProcessingService : IJobProcessingService
{
    private readonly IPublishEndpoint publishEndpoint;

    public JobProcessingService(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async Task ProcessJobAsync(Guid jobId)
    {
        await this.publishEndpoint.Publish(new JobInProgressEvent
        {
            JobId = jobId,
        });
        await Task.Delay(5000);
        await this.publishEndpoint.Publish(new JobCompletedEvent
        {
            JobId = jobId,
            CompletedAt = DateTime.UtcNow,
            Result = new Random().Next(1, 101).ToString(),
        });
    }
}