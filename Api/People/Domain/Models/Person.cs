namespace DeliveryU.Api.People.Domain.Models;

public class Person
{
    public string Id { get; set;}
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required PersonType PersonType { get; set; }

    public Person()
    {
        Id = Guid.NewGuid().ToString();
    }

}