namespace DeliveryU.Api.Security.AccessControl.Domain.Services;

public interface ICryptoService
{
    string HashPassword(string plain_password);
    bool VerifyPassword(string password, string hashed_password);
}