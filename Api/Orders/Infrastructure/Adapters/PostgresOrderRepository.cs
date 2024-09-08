using DeliveryU.Api.Orders.Domain.Models;
using DeliveryU.Api.Orders.Domain.Repositories;
using DeliveryU.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace DeliveryU.Api.Orders.Infrastructure.Adapters;

public class PostgresOrderRepository : IOrderRepository
{
    private readonly DatabaseManager _db;

    public PostgresOrderRepository(DatabaseManager db)
    {
        _db = db;
    }

    public void CreateOrder(Order order)
    {
        _db.Order.Add(order);
        _db.SaveChanges();
    }

    public void UpdateOrder(Order order)
    {
        var orderUpdate = _db.Order.FirstOrDefault(o => o.Id == order.Id);
        orderUpdate = order;
        _db.SaveChanges();
    }

     public Order? FetchOrderById(string orderId)
    {
        return _db.Order.Include(o => o.Client).FirstOrDefault(o => o.Id == orderId);
    }
      public Order? FetchOrderByIdDelivery(string orderId)
    {
        return _db.Order.Include(o => o.Delivery).FirstOrDefault(o => o.Id == orderId);
    }

     public ICollection<Order> FetchOrderAssigned(string idPerson)
    {
        return _db.Order.Include(o => o.Client).Where(o=> o.Client!= null && o.Client.Id == idPerson).ToList();
    }

    public ICollection<Order> FetchOrderAssignedToDelivery(string idPerson)
    {
        return _db.Order.Include(o => o.Delivery).Where(o => o.Delivery != null && o.Delivery.Id == idPerson).ToList();
    }

    public ICollection<Order> FetchOrderCompleted()
    {
        return _db.Order.Include(o => o.Client).Where(o => o.Client != null && o.Delivery != null).ToList();
    }

    public ICollection<Order> FetchOrderCreated()
    {
        return _db.Order.Include(o => o.Client).Include(o => o.Delivery).Where(o => o.Delivery == null).ToList();
    }
}