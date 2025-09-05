namespace InterviewGuide.Responses;

public class ErrorResponse
{
    public string ErrorMessage { get; init; } = null!;

    public string? Details { get; init; }
}