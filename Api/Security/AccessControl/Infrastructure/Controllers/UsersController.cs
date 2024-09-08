using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Services;
using DeliveryU.Api.Security.AccessControl.Infrastructure.Dtos;
using DeliveryU.Api.Security.AccessControl.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryU.Api.Security.AccessControl.Infrastructure.Controllers;

[UserRegisteredFilter]
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UsersController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public IActionResult Register(
        [FromBody] UserRegisterRequestDto request
    )
    {
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            Person = new Person
            {
                Name = request.Name,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PersonType = PersonType.Client
            }
        };
        _userService.Register(user);
        return Created();
    }

    [HttpPost("login")]
    public IActionResult Login(
        [FromBody] LoginRequestDto request
    )
    {
        var user = _userService.Login(request.Email, request.Password);
        if (user == null)
        {
            return Forbid();
        }

        return Ok(new JwtDto
        {
            Token = _tokenService.GenerateToken(user.Person.Id)
        });
    }

}