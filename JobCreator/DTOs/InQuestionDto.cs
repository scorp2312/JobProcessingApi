namespace JobCreator.DTOs;

public class InQuestionDto
{
    public Guid Id { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public InQCategoryDto? Category { get; set; }
}