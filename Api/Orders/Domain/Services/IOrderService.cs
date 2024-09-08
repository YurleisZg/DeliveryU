using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.Orders.Domain.Services;

public interface IOrderService 
{
    void CreateOrder (Order order);
    int CalculateTotalPrice(ICollection<OrderDetail> orderDetails, int shippingPrice);
    void AcceptedOrder(Person person, Order order);
    void ConfirmOrder(Order order);
    void UpdateOrder(Order order);
    Order? FetchOrderById (string orderId);
    ICollection<Order> FetchOrderCreated();
    ICollection<Order> FetchOrderAssigned(string idPerson);
    ICollection<Order> FetchOrderAssignedToDelivery(string idPerson);
    ICollection<Order> FetchOrderCompleted();
}