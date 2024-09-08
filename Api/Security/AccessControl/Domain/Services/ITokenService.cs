namespace DeliveryU.Api.Security.AccessControl.Domain.Services;

public interface ITokenService
{
    string GenerateToken(string id);
}