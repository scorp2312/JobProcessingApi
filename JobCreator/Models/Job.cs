namespace JobCreator.Models;

public enum JobStatus
{
    Created = 0,

    InProgress = 1,

    Completed = 2,
}

public class Job
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public JobStatus Status { get; set; }

    public string? Description { get; set; }

    public string? Result { get; set; }
}