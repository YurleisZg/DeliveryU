using DeliveryU.Api.OrdersDetails.Domain.Models;
using DeliveryU.Api.OrdersDetails.Domain.Repositories;
using DeliveryU.Api.OrdersDetails.Domain.Services;

namespace DeliveryU.Api.OrdersDetails.Application;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public void SaveDetail(OrderDetail OrderDetail)
    {
        _orderDetailRepository.SaveDetail(OrderDetail);
    }
}