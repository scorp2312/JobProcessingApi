namespace JobCreator.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }

    public string? QuestionText { get; set; }

    public string? Answer { get; set; }

    public CategoryDto? Category { get; set; }
}