namespace JobCreator.Models;

public class Question
{
    public required Guid Id { get; set; }

    public required string? QuestionText { get; set; }

    public string? Answer { get; set; }

    public required Category Category { get; set; }
}