namespace InterviewGuide.Domain.Entities;

public class QuestionEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string QuestionText { get; set; }

    public required string Answer { get; set; }

    public required CategoryEntity CategoryEntity { get; set; }
}