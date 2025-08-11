namespace Shared.Messages.Events;

public class JobCompletedEvent
{
    public Guid JobId { get; set; }

    public DateTime CompletedAt { get; set; }

    public string? Result { get; set; }
}