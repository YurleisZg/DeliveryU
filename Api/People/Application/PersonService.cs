using DeliveryU.Api.People.Domain.Models;
using DeliveryU.Api.People.Domain.Repositories;
using DeliveryU.Api.People.Domain.Services;

namespace DeliveryU.Api.People.Application;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Person? FetchPersonById(string id)
    {
        return _personRepository.FetchPersonById(id);
    }

    public bool OutdatedDelivery(string id)
    {
        var personUpdate = _personRepository.FetchPersonById(id);
        if (personUpdate != null && personUpdate!.PersonType == PersonType.Delivery)
        {
             personUpdate.PersonType = PersonType.Client;
            _personRepository.UpdatePerson(personUpdate);
            return true;
        }
        return false;
    }

    public bool UpdateToDelivery(string id)
    {
        var person = _personRepository.FetchPersonById(id);
        if (person == null)
        {
            return false;
        }
        person.PersonType = PersonType.Delivery;
        _personRepository.UpdatePerson(person);
        return true;
    }
}