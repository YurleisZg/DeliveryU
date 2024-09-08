using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.Orders.Domain.Models;
public class Order
{
    public string Id { get; set; }
    public required Person Client { get; set; }
    public Person? Delivery { get; set; }
    public required string ShippingAddress { get; set; }
    public required int ShippingPrice { get; set; }
    public required PayMethod PayMethod { get; set; }
    public required int TotalPrice { get; set; }
    public required OrderState OrderState { get; set; }
    public required DateTime DateTime { get; set; }
    public required string Observations { get; set; }
    public required Boolean ClientConfirmation {get; set;}
    public required Boolean DeliveryConfirmation {get; set;}

    public Order()
    {
        Id = Guid.NewGuid().ToString();
    }
}