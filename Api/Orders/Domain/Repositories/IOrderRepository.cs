using DeliveryU.Api.Orders.Domain.Models;

namespace DeliveryU.Api.Orders.Domain.Repositories;

public interface IOrderRepository 
{
    void CreateOrder(Order order);
    void UpdateOrder(Order order);
    ICollection<Order> FetchOrderCreated();
    ICollection<Order> FetchOrderAssignedToDelivery(string idPerson);
    ICollection<Order> FetchOrderAssigned(string idPerson);
    ICollection<Order> FetchOrderCompleted();
    Order? FetchOrderById(string orderId);
    Order? FetchOrderByIdDelivery(string orderId);
}