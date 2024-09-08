namespace DeliveryU.Api.Security.AccessControl.Infrastructure.Dtos;

public class LoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}