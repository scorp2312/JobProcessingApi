namespace InterviewGuide.Application.Models;

public class CommentDto
{
    public required Guid Id { get; init; }

    public required string Author { get; init; }

    public required string Content { get; init; }

    public required DateTime Created { get; init; }
}