using Models;

namespace LN.Interfaces
{
    public interface IPersonService
    {
        List<Person> GetAllPersons();
        Person GetPersonById(int id);
        Person AddPerson(Person person);
        bool UpdatePerson(Person person);
        bool DeletePerson(int id);
    }
}
