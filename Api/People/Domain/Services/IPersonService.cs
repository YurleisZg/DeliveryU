using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.People.Domain.Services;

public interface IPersonService 
{
    Person? FetchPersonById(string id);
    bool UpdateToDelivery(string id);
    bool OutdatedDelivery(string id);
}