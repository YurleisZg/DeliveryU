namespace DeliveryU.Api.Stores.Domain.Models;

public class Store
{
    public string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? ImageUrl{get; set;}

    public Store()
    {
        Id = Guid.NewGuid().ToString();
    }
}