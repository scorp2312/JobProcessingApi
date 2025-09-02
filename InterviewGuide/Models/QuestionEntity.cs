namespace InterviewGuide.Models;

public class QuestionEntity
{
    public required Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public required string Answer { get; set; }

    public required CategoryEntity CategoryEntity { get; set; }
}