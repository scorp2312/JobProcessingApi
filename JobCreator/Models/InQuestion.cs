namespace JobCreator.Models;

public class InQuestion
{
    public required Guid Id { get; set; }

    public required string? Question { get; set; }

    public string? Answer { get; set; }

    public required InQCategory Category { get; set; }
}