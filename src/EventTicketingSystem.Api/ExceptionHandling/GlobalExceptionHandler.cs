using EventTicketingSystem.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace EventTicketingSystem.Api.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors.GroupBy(error => error.PropertyName);

                var errorNamesAndMessages = errors.ToDictionary(
                    errorName => errorName.Key,
                    error => error.Select(error => error.ErrorMessage).ToArray()
                    );

                var responseObject = new
                {
                    status = httpContext.Response.StatusCode,
                    title = "Validation Failed",
                    errors = errorNamesAndMessages
                };

                await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);

                return true;
            }
            else if (exception is NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                var responseObject = new
                {
                    status = httpContext.Response.StatusCode,
                    title = "Not Found",
                    error = notFoundException.Message
                };

                await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);

                return true;
            }
            else if (exception is SeatAlreadyExistsException seatAlreadyExistsException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                var responseObject = new
                {
                    status = httpContext.Response.StatusCode,
                    title = "Resource Already Exists",
                    error = seatAlreadyExistsException.Message
                };

                await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
