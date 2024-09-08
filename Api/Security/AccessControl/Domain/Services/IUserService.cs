using DeliveryU.Api.Security.AccessControl.Domain.Models;

namespace DeliveryU.Api.Security.AccessControl.Domain.Services;

public interface IUserService
{
    void Register(User user);

    User? Login(string email, string password);
}