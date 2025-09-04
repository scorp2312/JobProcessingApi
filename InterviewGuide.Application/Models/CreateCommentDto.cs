namespace InterviewGuide.Application.Models;

public class CreateCommentDto
{
    public required string Author { get; init; }

    public required string Content { get; init; }
}