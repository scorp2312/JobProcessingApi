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
        catch (NotFoundException exception)
        {
            this.logger.LogError(exception, exception.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            var errorResponse = new ErrorResponse
            {
                ErrorMessage = exception.Message,
            };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
        catch (BusinessException exception)
        {
            this.logger.LogError(exception, exception.ErrorMessage);
            context.Response.StatusCode = exception.ErrorStatusCode;

            await context.Response.WriteAsJsonAsync(exception.ErrorMessage);
        }
        catch (Exception exception)
        {
            this.logger.LogError(exception, exception.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}