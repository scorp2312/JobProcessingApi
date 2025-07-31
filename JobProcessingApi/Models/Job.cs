namespace JobProcessingApi.Models;

public class Job
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public JobStatus Status { get; set; }
}

public enum JobStatus
{
    Created = 0,
    InProgress = 1,
    Completed = 2
}