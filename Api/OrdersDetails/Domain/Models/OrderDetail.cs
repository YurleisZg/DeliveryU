using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.Products.Domain.Models;

namespace DeliveryU.Api.OrdersDetails.Domain.Models;

public class OrderDetail
{
    public string Id { get; set; }
    public required int Quantity { get; set; }
    public required Product Product { get; set; }
    public required Order Order { get; set; }

    public OrderDetail()
    {
        Id = Guid.NewGuid().ToString();
    }
}