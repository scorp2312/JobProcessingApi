using JobProcessingApi.Messages;
using MassTransit;

namespace JobProcessingApi.Consumers;

public class JobCreatedConsumer : IConsumer<JobCreatedEvent>
{
    public JobCreatedConsumer()
    {
    }

    public Task Consume(ConsumeContext<JobCreatedEvent> context)
    {
        return Task.CompletedTask;
    }
}