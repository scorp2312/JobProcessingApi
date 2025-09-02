namespace InterviewGuide.Application.Models;

public class QuestionDto
{
    public Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public required string Answer { get; set; }

    public required int CategoryId { get; set; }
}