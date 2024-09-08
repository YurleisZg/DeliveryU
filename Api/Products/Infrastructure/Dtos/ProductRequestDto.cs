using DeliveryU.Api.Products.Domain.Models;

namespace DeliveryU.Api.Products.Infrastructure.Dtos;

public class ProductRequestDto
{
    public required ICollection<Product> Products {get; set;}
}