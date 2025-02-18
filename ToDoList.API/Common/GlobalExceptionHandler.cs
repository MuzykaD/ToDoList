using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Exceptions;

namespace ToDoList.API.Common;

//Use validators in order to drop and catch ValidationException from FluentValidation

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails();

        if (exception is NotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            problemDetails.Title = "NotFound";
            problemDetails.Extensions.Add(nameof(notFoundException.ProvidedId), notFoundException.ProvidedId);
        }
        else if (exception is BadRequestException badRequestException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails.Title = "BadRequest";
            problemDetails.Extensions.Add(nameof(badRequestException.FaultObject), badRequestException.FaultObject);
        }       
        else
        {
            problemDetails.Title = "An Exception occurred!";
            problemDetails.Detail = exception.Message;
        }

        problemDetails.Instance = httpContext.Request.Path;
        problemDetails.Status = httpContext.Response.StatusCode;
        problemDetails.Detail = exception.Message;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}
