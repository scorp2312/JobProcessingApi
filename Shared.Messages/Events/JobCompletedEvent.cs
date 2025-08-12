namespace Shared.Messages.Events;

public class JobCompletedEvent
{
    public required Guid JobId { get; init; }

    public required DateTime CompletedAt { get; init; }

    public required string? Result { get; init; }
}