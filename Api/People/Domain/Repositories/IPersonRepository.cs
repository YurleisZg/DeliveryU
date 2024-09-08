using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.People.Domain.Repositories;

public interface IPersonRepository 
{
    Person? FetchPersonById(string id);
    void UpdatePerson(Person person);
    void OutdatedDelivery(Person person);
}