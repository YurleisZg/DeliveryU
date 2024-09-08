using DeliveryU.Api.OrdersDetails.Domain.Models;

namespace DeliveryU.Api.OrdersDetails.Domain.Repositories;

public interface IOrderDetailRepository
{
    void SaveDetail(OrderDetail OrderDetail);
}