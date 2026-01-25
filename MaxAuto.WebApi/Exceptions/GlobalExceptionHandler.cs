using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using MaxAuto.WebApi.Domain.Models.Responses;

namespace MaxAuto.WebApi.Exceptions;

/// <summary>
/// Global exception handler that logs exceptions and returns appropriate error responses.
/// </summary>
/// <param name="logger">The logger instance.</param>
public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Tries to handle the given exception and write an appropriate response to the HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task indicating whether the exception was handled.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);
        var response = new ErrorResponse
        {
            Message = exception.Message
        };

        switch (exception)
        {
            case BadHttpRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Titel = exception.GetType().Name;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Titel = "Internal Server Error";
                break;
        }

        httpContext.Response.StatusCode = response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
