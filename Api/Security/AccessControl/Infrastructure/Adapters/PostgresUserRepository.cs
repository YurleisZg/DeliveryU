using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Models;
using DeliveryU.Api.Security.AccessControl.Domain.Repositories;
using DeliveryU.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace DeliveryU.Api.Security.AccessControl.Infrastructure.Adapters;

public class PostgresUserRepository : IUserRepository
{
    private readonly DatabaseManager _db;

    public PostgresUserRepository(DatabaseManager db)
    {
        _db = db;
    }

    public User? FetchLoginByEmail(string email)
    {
        return _db.User.Include(loginUser => loginUser.Person).FirstOrDefault(loginUser => loginUser.Email == email);
    }

    public void SavePerson(Person person)
    {
        _db.Person.Add(person);
        _db.SaveChanges();
    }

    public void SaveUser(User user)
    {
        _db.User.Add(user);
        _db.SaveChanges();
    }

    public ICollection<User> ShowAll()
    {
        return _db.User.Include(u => u.Person).ToList();
    }
}