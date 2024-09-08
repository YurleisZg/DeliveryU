using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.People.Domain.Repositories;
using DeliveryU.Persistence.Database;

namespace DeliveryU.Api.People.Infrastructure.Adapters;

public class PostgresPersonRepository : IPersonRepository
{
    private readonly DatabaseManager _db;

    public PostgresPersonRepository(DatabaseManager db)
    {
        _db = db;
    }
    public Person? FetchPersonById(string id)
    {
        return _db.Person.FirstOrDefault(p => p.Id == id);
    }

    public void OutdatedDelivery(Person person)
    {
        _db.Person.Find(person);
       _db.SaveChanges();
    }

    public void UpdatePerson(Person person)
    {
       var personUpdate = _db.Person.FirstOrDefault(p => person.Id == p.Id);
       personUpdate = person;
       _db.SaveChanges();
    }
}