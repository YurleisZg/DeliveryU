using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeliveryU.Api.Security.AccessControl.Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryU.Api.Security.AccessControl.Application;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string id)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF32.GetBytes(_configuration["Token:SecretKey"]!));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims = [new(type: "id", value: id)];

        var expiration = DateTime.Now.AddHours(24);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: expiration,
            signingCredentials: signingCredentials,
            claims: claims
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(securityToken);
    }
}