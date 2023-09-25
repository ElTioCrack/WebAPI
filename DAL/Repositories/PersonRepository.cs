using Models;

using DAL.Interfaces;

using Tools.Encryption;

using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

		public List<Person> GetAllPersons()
		{
			return _context.Persons.ToList();
		}

		public Person GetPersonById(int id)
		{
			var person = _context.Persons.FirstOrDefault(p => p.Id == id);

			if (person == null)
			{
				throw new InvalidOperationException($"No se encontró una persona con el ID {id}");
				// También puedes devolver null o un valor predeterminado en lugar de lanzar una excepción,
				// dependiendo de cómo desees manejar esta situación.
			}

			return person;
		}

		public Person AddPerson(Person person)
		{
			string hashedPassword = Argon2Hasher.GenerateHash(person.Password);
			person.Password = hashedPassword;
			_context.Persons.Add(person);
			_context.SaveChanges();
			return person;
		}


		public bool UpdatePerson(Person person)
        {
			if (person == null)
			{
				throw new ArgumentNullException(nameof(person));
			}

			var existingPerson = _context.Persons.Find(person.Id);

			if (existingPerson == null)
			{
				return false; // La persona no existe en la base de datos
			}

			// Actualiza las propiedades de la persona existente
			existingPerson.Username = person.Username;
			existingPerson.Password = person.Password;

			_context.Persons.Update(existingPerson);
			_context.SaveChanges();

			return true;
		}
		public bool DeletePerson(int id)
		{
			var existingPerson = _context.Persons.Find(id);

			if (existingPerson == null)
			{
				return false; // La persona no existe en la base de datos
			}

			_context.Persons.Remove(existingPerson);
			_context.SaveChanges();

			return true;
		}

	}
}
