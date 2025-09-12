namespace InterviewGuide.Domain.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string errorMessage, int errorStatusCode, string? details)
    {
        this.ErrorMessage = errorMessage;
        this.ErrorStatusCode = errorStatusCode;
        this.Details = details;
    }

    public BusinessException(string errorMessage, int errorStatusCode)
    {
        this.ErrorMessage = errorMessage;
        this.ErrorStatusCode = errorStatusCode;
    }

    public string ErrorMessage { get; set; }

    public string? Details { get; set; }

    public int ErrorStatusCode { get; set; }
}