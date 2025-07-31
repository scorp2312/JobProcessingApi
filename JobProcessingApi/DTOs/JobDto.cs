namespace JobProcessingApi.DTOs;

public class JobDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateJobDto
{
    public string? Description { get; set; }
}