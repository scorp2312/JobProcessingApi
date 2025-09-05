namespace InterviewGuide.Responses;

public class ErrorResponse
{
    public required string ErrorMessage { get; init; } = null!;

    public string? Details { get; init; }
}