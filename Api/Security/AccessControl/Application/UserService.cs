using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Repositories;
using DeliveryU.Api.Security.AccessControl.Domain.Services;

namespace DeliveryU.Api.Security.AccessControl.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ICryptoService _crypto;

    public UserService(IUserRepository repository, ICryptoService crypto)
    {
        _crypto = crypto;
        _repository = repository;
    }

    public User? Login(string email, string password)
    {
        var login = _repository.FetchLoginByEmail(email);

        if (login == null)
        {
            return null;
        }

        var valid = _crypto.VerifyPassword(password, login.Password!);
        if (!valid)
        {
            return null;
        }

        return login;
    }

    public void Register(User user)
    {
        var person = new Person
        {
            Name = user.Person.Name,
            LastName = user.Person.LastName,
            PhoneNumber = user.Person.PhoneNumber,
            PersonType = PersonType.Client
        };

        var user_login = new User
        {
            Person = person,
            Email = user.Email,
            Password = _crypto.HashPassword(user.Password!)
        };

        _repository.SavePerson(person);
        _repository.SaveUser(user_login);
    }
}