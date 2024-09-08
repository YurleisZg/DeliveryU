using DeliveryU.Api.Stores.Domain.Models;

namespace DeliveryU.Api.Products.Domain.Models;
public class Product
{
    public string Id { get; set; }
    public required Store Store { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Price { get; set; }
    public required string Category { get; set; }
    public string? ImageUrl {get; set;}

     public Product()
    {
        Id = Guid.NewGuid().ToString();
    }
}