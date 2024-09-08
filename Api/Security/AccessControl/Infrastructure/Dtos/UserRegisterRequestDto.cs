using System.ComponentModel.DataAnnotations;
using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;

namespace DeliveryU.Api.Security.AccessControl.Infrastructure.Dtos;

public class UserRegisterRequestDto
{
    public required string Name { get; set; }
    public required string LastName { get; set; }

    [RegularExpression(pattern: "3\\d{9}", ErrorMessage = "El número ingresado no es válido")]
    public required string PhoneNumber { get; set; }

    [EmailAddress(ErrorMessage = "El formato de correo ingresado no es válido")]
    public required string Email { get; set; }

    [RegularExpression(pattern: ".{8,}", ErrorMessage = "La contraseña debe tener mínimo 8 caracteres")]
    public required string Password { get; set; }
}

public class UserDto{
    public required ICollection<User> Users { get; set; }
}

public class PersonDto
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required PersonType PersonType { get; set; }
}