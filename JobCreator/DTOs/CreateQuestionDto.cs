namespace JobCreator.DTOs;

public class CreateQuestionDto
{
    public required string QuestionText { get; set; }

    public string? Answer { get; set; }

    public required int CategoryId { get; set; }
}