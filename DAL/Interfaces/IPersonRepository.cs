using Models;

namespace DAL.Interfaces
{
    public interface IPersonRepository
    {
		List<Person> GetAllPersons();
		Person GetPersonById(int id);
		Person AddPerson(Person person);
		bool UpdatePerson(Person person);
		bool DeletePerson(int id);
	}
}
