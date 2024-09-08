using System.Text.Json;
using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.Orders.Domain.Services;
using DeliveryU.Api.Orders.Infrastructure.Dtos;
using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.OrdersDetails.Domain.Services;
using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.People.Domain.Services;
using DeliveryU.Api.Products.Domain.Services;
using DeliveryU.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryU.Api.Orders.Infrastructure.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IPersonService _personService;
    private readonly INotificationService _notificationService;
    
    public OrdersController(IOrderService orderService, IProductService productService, IOrderDetailService orderDetailService, 
    IPersonService personService, INotificationService notificationService)
    {
        _orderService = orderService;
        _productService = productService;
        _orderDetailService = orderDetailService;
        _personService = personService;
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpPost ("order")]
    public IActionResult CreateOrder (
        [FromBody] OrderRequestDto request
    )
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if(userId == null)
        {
            return Unauthorized();
        }

        var order = new Order
        {
           Client = _personService.FetchPersonById(userId)!,
           Delivery = null,
           ShippingAddress = request.ShippingAddress,
           ShippingPrice = request.ShippingPrice,
           PayMethod = request.PayMethod,
           DateTime = request.DateTime,
           Observations = request.Observations,
           OrderState = OrderState.Created,
           TotalPrice = 0,
           ClientConfirmation = false,
           DeliveryConfirmation = false
        };

        var products = _productService.SearchInListById(request.Products.Select(p => p.ProductId).ToList());

        var orderDetails = new List<OrderDetail>();

        foreach(var product in request.Products) {
            orderDetails.Add(
                item: new OrderDetail {
                    Quantity = product.Quantity,
                    Product = _productService.FetchProductById(product.ProductId),
                    Order = order
                }
            );
        }

        order.TotalPrice = _orderService.CalculateTotalPrice(orderDetails, request.ShippingPrice);

        if (order == null)
        {
            return Forbid();
        }

        _orderService.CreateOrder(order);
        foreach(var orderDetail in orderDetails) {
            _orderDetailService.SaveDetail(orderDetail);
        }
        
        _notificationService.SubscribeToTopicAsync(userId, order.Id);
        _notificationService.SendNotificationToTopicAsync("delivery", "Nuevo Pedido", "Se ha creado un nuevo pedido");

        return Created();
    }

    [Authorize]
    [HttpPut ("acept/{orderId}")]
    public IActionResult OrderUpdate(string orderId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if(userId == null)
        {
            return Unauthorized();
        }

        var person = _personService.FetchPersonById(userId);
        
        if (person == null)
        {
            return NotFound("Persona no encontrada");
        }

        if (person.PersonType != PersonType.Delivery)
        {
            return Forbid();
        }
        
        var order = _orderService.FetchOrderById(orderId);

        if (order == null)
        {
            return NotFound("Pedido no encontrado");
        }

        if(order.Client.Id == person.Id) {
            return Forbid();
        }

        if(order.OrderState != OrderState.Created)
        {
            return Forbid();
        }

        _orderService.AcceptedOrder(person, order);
        _notificationService.SubscribeToTopicAsync(userId, order.Id);
        _notificationService.SendNotificationToTopicAsync(order.Id, "Pedido aceptado", "Tu pedido fue aceptado por un repartidor.");
    
        return Ok();
    }

    [Authorize]
    [HttpPut ("confirm/{orderId}")]
    public IActionResult ConfirmOrder(string orderId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if(userId == null)
        {
            return Unauthorized();
        }

        var person = _personService.FetchPersonById(userId);
        
        if (person == null)
        {
            return NotFound("Persona no encontrada");
        }
        
        var order = _orderService.FetchOrderById(orderId);

        if (order == null)
        {
            return NotFound("Pedido no encontrado");
        }

        if(order.OrderState != OrderState.Assigned) 
        {
            return Forbid();
        }

        if(order.Client.Id == userId && order.ClientConfirmation == false) 
        {
            _notificationService.SendNotificationToTopicAsync(
                order.Id, 
                "El cliente confirmó el pedido", 
                "No olvides confirmarlo también"
            );
            order.ClientConfirmation = true;
            _orderService.UpdateOrder(order);
            if(order.DeliveryConfirmation == true) 
            {
                _orderService.ConfirmOrder(order);
                _notificationService.SendNotificationToTopicAsync(order.Id, "Pedido completado", "Se ha completado su pedido con éxito");
            }
            return Ok();
        }

        if(order.Delivery!.Id == userId && order.DeliveryConfirmation == false) 
        {
            _notificationService.SendNotificationToTopicAsync(
                order.Id, 
                "El delivery confirmó el pedido", 
                "No olvides confimarlo tambié"
            );
            order.DeliveryConfirmation = true;
            _orderService.UpdateOrder(order);
            
            if(order.ClientConfirmation == true) {
                _orderService.ConfirmOrder(order);
                _notificationService.SendNotificationToTopicAsync(order.Id, "Pedido completado", "Se ha completado su pedido con éxito");
            }
            return Ok();
        }

        _orderService.ConfirmOrder(order);
        _notificationService.SendNotificationToTopicAsync(order.Id, "Pedido completado", "Se ha completado su pedido con éxito");
        return Ok();
    }

    [Authorize]
    [HttpGet("created")]
    public IActionResult FetchOrderCreated ()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if(userId == null)
        {
            return Unauthorized();
        }

        var person = _personService.FetchPersonById(userId);
        
        if (person == null)
        {
            return NotFound("Persona no encontrada");
        }

        if (person.PersonType != PersonType.Delivery)
        {
            return Forbid();
        }

       var orderCreated =  _orderService.FetchOrderCreated();
        return Ok(orderCreated);
    }
     [Authorize]
    [HttpGet("assigned")]
    public IActionResult FetchOrderAssigned()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var person = _personService.FetchPersonById(userId);

        if(person!.PersonType == PersonType.Delivery)
        {
            var orders = _orderService.FetchOrderAssignedToDelivery(userId);
            var assigned = orders.Where(o => o.Delivery == null).ToList();
            
            if (!assigned.Any())
            {
                return NotFound("No tienes pedidos asignados");
            }

            return Ok(assigned);

        }

        if(person.PersonType == PersonType.Client)
        {
            var orders = _orderService.FetchOrderAssigned(userId);

            if ( orders == null || !orders.Any())
            {
                return NotFound("Primero debes crear un pedido");
            }
            
            var assignedOrders = orders.Where(o => o.Delivery != null).ToList();
             if (!assignedOrders.Any())
            {
                return NotFound("El pedido no ha sido asignado a un repartidor");
            }
            return Ok(assignedOrders);
        }
        return Forbid();
    }

    [Authorize]
    [HttpGet("completed")]
    public IActionResult FetchOrderCompleted()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if(userId == null)
        {
            return Unauthorized();
        }

        var person = _personService.FetchPersonById(userId);
        
        if (person == null)
        {
            return NotFound("Persona no encontrada");
        }

        var orderCompleted =  _orderService.FetchOrderCompleted();
        if(orderCompleted == null)
        {
            return Forbid();
        }
        return Ok(orderCompleted);
    }

}