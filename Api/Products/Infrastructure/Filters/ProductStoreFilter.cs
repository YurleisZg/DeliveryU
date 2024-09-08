using DeliveryU.Shared.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeliveryU.Api.Products.Infrastructure.Filters;

public class ProductStoreFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        ExceptionHandler.HandleException(context, new ExceptionResponse
        {
            Code = StatusCodes.Status400BadRequest,
            Message = "La Tienda solicitada no tiene productos disponibles"
        });

        base.OnException(context);
    }
}