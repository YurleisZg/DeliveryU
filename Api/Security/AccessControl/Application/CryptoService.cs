using DeliveryU.Api.Security.AccessControl.Domain.Services;

namespace DeliveryU.Api.Security.AccessControl.Application;

public class CryptoService : ICryptoService
{
    public string HashPassword(string plain_password)
    {
        return BCrypt.Net.BCrypt.HashPassword(plain_password);
    }

    public bool VerifyPassword(string password, string hashed_password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashed_password);
    }
}