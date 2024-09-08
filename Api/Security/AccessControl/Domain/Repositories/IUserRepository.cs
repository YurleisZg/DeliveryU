using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;

namespace DeliveryU.Api.Security.AccessControl.Domain.Repositories;

public interface IUserRepository
{
    User? FetchLoginByEmail(string email);
    void SavePerson(Person person);
    void SaveUser(User user);
}