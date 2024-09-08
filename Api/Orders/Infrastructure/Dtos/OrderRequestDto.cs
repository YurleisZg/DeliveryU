using System.ComponentModel.DataAnnotations;
using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.OrdersDetails.Infrastructure.Dtos;

namespace DeliveryU.Api.Orders.Infrastructure.Dtos;

public class OrderRequestDto 
{
    public required string ShippingAddress { get; set;}

    [Range(1000, 500000)]
    public required int ShippingPrice { get; set; }
    public required PayMethod PayMethod { get; set;}
    public required DateTime DateTime { get; set; }
    public required string Observations { get; set; }
    public required ICollection<OrderDetailRequestDto> Products {get; set;}
}