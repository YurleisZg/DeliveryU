using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.OrdersDetails.Domain.Repositories;
using DeliveryU.Persistence.Database;

namespace DeliveryU.Api.OrdersDetails.Infrastructure.Adapter;

public class PostgresOrderDetailRepository : IOrderDetailRepository
{
    private readonly DatabaseManager _db;

    public PostgresOrderDetailRepository(DatabaseManager db)
    {
        _db = db;
    }

    public void SaveDetail(OrderDetail OrderDetail)
    {
        _db.OrderDetail.Add(OrderDetail);
        _db.SaveChanges();
    }
}