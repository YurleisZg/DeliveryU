namespace DeliveryU.Api.People.Infrastructure.Dtos;

public class PersonRequestDto 
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
}