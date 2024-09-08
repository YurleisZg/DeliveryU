using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.Orders.Domain.Repositories;
using DeliveryU.Api.Orders.Domain.Services;
using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.Orders.Application;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public void AcceptedOrder(Person person, Order order)
    {
        order.Delivery = person;
        order.OrderState = OrderState.Assigned;
        _orderRepository.UpdateOrder(order);
    }

    public int CalculateTotalPrice(ICollection<OrderDetail> orderDetails, int shippingPrice)
    {
         return orderDetails.Select(od => od.Product.Price * od.Quantity).Sum() + shippingPrice;
    }

    public void ConfirmOrder(Order order)
    {
        order.OrderState = OrderState.Completed;
        _orderRepository.UpdateOrder(order);
    }

    public void CreateOrder(Order order)
    {
        _orderRepository.CreateOrder(order);
    }

    public ICollection<Order> FetchOrderAssigned(string idPerson)
    {
         return _orderRepository.FetchOrderAssigned(idPerson);
    }

    public ICollection<Order> FetchOrderAssignedToDelivery(string idPerson)
    {
       return _orderRepository.FetchOrderAssignedToDelivery(idPerson);
    }

    public Order? FetchOrderById(string orderId)
    {
         return _orderRepository.FetchOrderById(orderId);
    }

    public ICollection<Order> FetchOrderCompleted()
    {
        return _orderRepository.FetchOrderCompleted();
    }

    public ICollection<Order> FetchOrderCreated()
    {
        return _orderRepository.FetchOrderCreated();
    }

    public void UpdateOrder(Order order)
    {
        _orderRepository.UpdateOrder(order);
    }
}