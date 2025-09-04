namespace InterviewGuide.Application.Models;

public class CommentDto
{
    public Guid Id { get; set; }

    public required string Author { get; set; }

    public required string Content { get; set; }

    public DateTime Created { get; set; }
}