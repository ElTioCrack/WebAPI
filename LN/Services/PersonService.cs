using Models;

using DAL.Interfaces;

using LN.Interfaces;

namespace LN.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

		public List<Person> GetAllPersons()
		{
			return _personRepository.GetAllPersons();
		}

		public Person GetPersonById(int id)
        {
			return _personRepository.GetPersonById(id);
		}

		public Person AddPerson(Person person)
		{
			// Realiza la lógica de negocio antes de agregar la persona, si es necesario.
			// Por ejemplo, puedes validar los datos, verificar duplicados, etc.
			if (string.IsNullOrWhiteSpace(person.Username) || string.IsNullOrWhiteSpace(person.Password))
			{
				// Puedes lanzar una excepción o manejar la validación de acuerdo a tus requisitos.
				throw new ArgumentException("El nombre de usuario y la contraseña son obligatorios.");
			}

			// Llama al método AddPerson del repositorio para agregar la persona a la base de datos
			var addedPerson = _personRepository.AddPerson(person);

			// Realiza más operaciones si es necesario después de agregar la persona
			// Por ejemplo, realizar registro de auditoría, notificar eventos, etc.
			// Puedes agregar esta lógica aquí.

			return addedPerson;
		}

		public bool UpdatePerson(Person person)
		{
			// Llama al método de la DAL para actualizar la persona en la base de datos
			bool updatedPerson = _personRepository.UpdatePerson(person);

			if (updatedPerson)
			{
				// La actualización se realizó con éxito
				// Aquí puedes implementar lógica de negocios adicional si es necesario

				// Por ejemplo, podrías registrar un evento de auditoría
				// Log.Audit($"Se actualizó la persona con ID {person.Id}");

				// O notificar a otros sistemas o usuarios
				// NotificationService.NotifyUser(person.Id, "Se actualizó la información de la persona");

				// También puedes devolver algún valor o indicador adicional si es necesario
			}
			else
			{
				// La actualización falló o la persona no existe en la base de datos
				// Aquí también puedes implementar lógica de negocios si es necesario

				// Por ejemplo, podrías registrar un error
				// Log.Error($"No se pudo actualizar la persona con ID {person.Id}");

				// O notificar a otros sistemas o usuarios sobre el error
				// NotificationService.NotifyAdmin("Error al actualizar persona", "No se pudo actualizar la información de la persona");

				// También puedes devolver algún valor o indicador adicional si es necesario
			}

			// Devuelve el resultado de la actualización
			return updatedPerson;
		}


		public bool DeletePerson(int id)
        {
			// Validación de persona existente en la DAL
			return _personRepository.DeletePerson(id);
		}
    }
}
