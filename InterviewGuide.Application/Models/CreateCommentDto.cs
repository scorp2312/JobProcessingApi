namespace InterviewGuide.Application.Models;

public class CreateCommentDto
{
    public required string Author { get; set; }

    public required string Content { get; set; }
}