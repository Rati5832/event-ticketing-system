using EventTicketingSystem.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace EventTicketingSystem.Api.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private static (int statusCode, string title) MapException(Exception exception)
        {
            return exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                SeatAlreadyExistsException => (StatusCodes.Status409Conflict, "Resource Already Exists"),
                SeatNotInVenueException => (StatusCodes.Status400BadRequest, "Invalid Venue Seat"),
                SeatNotAvailableException => (StatusCodes.Status409Conflict, "Seat Not Available"),
                ConcurrencyException => (StatusCodes.Status409Conflict, "Concurrency Conflict"),
                InvalidReservationStateException => (StatusCodes.Status409Conflict, "Invalid Reservation State"),
                InvalidBookingException => (StatusCodes.Status400BadRequest, "Invalid Booking"),
                InvalidPaymentStateException => (StatusCodes.Status400BadRequest, "Invalid Payment State"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };
        }

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

                var response = new
                {
                    status = httpContext.Response.StatusCode,
                    title = "Validation Failed",
                    errors = errorNamesAndMessages
                };

                await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

                return true;
            }

            var (statusCode, errorTitle) = MapException(exception);
            httpContext.Response.StatusCode = statusCode;
            var responseObject = new
            {
                status = httpContext.Response.StatusCode,
                title = errorTitle,
                error = statusCode == StatusCodes.Status500InternalServerError ? "An unexpected error occurred." : exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);

            return true;
        }
    }
}
