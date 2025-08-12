namespace Shared.Messages.Events;

public class JobInProgressEvent
{
    public required Guid JobId { get; init; }
}