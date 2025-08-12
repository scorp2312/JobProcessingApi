namespace Shared.Messages.Events;

public class JobCreatedEvent
{
    public required Guid JobId { get; init; }
}