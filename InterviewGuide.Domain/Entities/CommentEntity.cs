namespace InterviewGuide.Domain.Entities;

public class CommentEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Author { get; set; }

    public required string Content { get; set; }

    public required DateTime Created { get; init; } = DateTime.UtcNow;

    public required QuestionEntity Question { get; init; }

    public required Guid QuestionId { get; init; }
}