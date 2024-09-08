using DeliveryU.Api.Stores.Domain.Models;

namespace DeliveryU.Api.Stores.Infrastructure.Dtos;

public class FetchStoresDto
{
    public required ICollection<Store> Stores { get; set; }
}