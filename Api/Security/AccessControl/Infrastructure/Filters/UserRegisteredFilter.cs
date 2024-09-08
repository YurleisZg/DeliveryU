using DeliveryU.Shared.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryU.Api.Security.AccessControl.Infrastructure.Filters;

public class UserRegisteredFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        ExceptionHandler.HandleException(context, new ExceptionResponse
        {
            Code = StatusCodes.Status400BadRequest,
            Message = "Usuario ya registrado"
        });

        base.OnException(context);
    }
}