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

            return false;
        }
    }
}
