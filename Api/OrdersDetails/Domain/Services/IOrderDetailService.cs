using DeliveryU.Api.OrdersDetails.Domain.Models;

namespace DeliveryU.Api.OrdersDetails.Domain.Services;

public interface IOrderDetailService
{
    void SaveDetail(OrderDetail OrderDetail);
}