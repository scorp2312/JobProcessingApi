namespace Shared.Messages;

public class JobCreatedEvent
{
    public Guid JobId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public string? Result { get; set; }
    
}

public class JobCompletedEvent
{
    public Guid JobId { get; set; }
    public DateTime CompletedAt { get; set; }
    public string? Result { get; set; }
}