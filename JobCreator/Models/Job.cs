namespace JobCreator.Models;

public class Job
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public JobStatus Status { get; set; }
    public string? Description { get; set; }
    public string? Result { get; set; } 
}

public enum JobStatus
{
    Created,
    InProgress,
    Completed
}