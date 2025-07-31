namespace JobProcessingApi.Messages;

public class JobCreatedEvent
{
    public Guid JobId { get; set; }
    public DateTime CreatedAt { get; set; }
}