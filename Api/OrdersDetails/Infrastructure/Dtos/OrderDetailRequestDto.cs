using System.ComponentModel.DataAnnotations;

namespace DeliveryU.Api.OrdersDetails.Infrastructure.Dtos;

public class OrderDetailRequestDto
{
    public required string ProductId {get; set;}

    [Range(1, 999999)]
    public required int Quantity {get; set;}
    
}