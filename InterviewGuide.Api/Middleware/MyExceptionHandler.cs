namespace InterviewGuide.Middleware;

using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Responses;

public class MyExceptionHandler(RequestDelegate next, ILogger<MyExceptionHandler> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger<MyExceptionHandler> logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception exception)
        {
            await this.HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception.GetType().IsGenericType &&
            exception.GetType().GetGenericTypeDefinition() == typeof(NotFoundException<>))
        {
            this.logger.LogError(exception, exception.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            var errorResponse = new ErrorResponse
            {
                ErrorMessage = exception.Message,
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}