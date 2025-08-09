namespace Shared.Messages;

public  class JobCreatedEvent
{
    public Guid JobId { get; set; }
}

public class JobCompletedEvent
{
    public Guid JobId { get; set; }
    public DateTime CompletedAt { get; set; }
    public string? Result { get; set; }
}

public class JobInProgressEvent
{
    public Guid JobId { get; set; }
}