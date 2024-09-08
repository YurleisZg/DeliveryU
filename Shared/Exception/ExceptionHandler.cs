using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryU.Shared.Exception;

public class ExceptionHandler
{
    public static void HandleException(ExceptionContext context, ExceptionResponse exceptionResponse)
    {
        var validationException = new ValidationException(exceptionResponse.Message);
        context.Exception = validationException;
        context.HttpContext.Response.StatusCode = exceptionResponse.Code;
        context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = exceptionResponse
        }));
        context.ExceptionHandled = true;
    }
}