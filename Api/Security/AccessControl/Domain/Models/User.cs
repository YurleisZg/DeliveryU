using DeliveryU.Api.People.Domain.Models;

namespace DeliveryU.Api.Security.AccessControl.Domain.Models;

public class User
{
    public string Id { get; set; }
    public required Person Person { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public User()
    {
        Id = Id = Guid.NewGuid().ToString();
    }
}