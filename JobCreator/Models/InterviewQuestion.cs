namespace JobCreator.Models;

public class InterviewQuestion
{
    public required Guid Id { get; set; }

    public required string Question { get; set; }

    public string? Answer { get; set; }

    public required InterviewQuestionCategory Category { get; set; }
}