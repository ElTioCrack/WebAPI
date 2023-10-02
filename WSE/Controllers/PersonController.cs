using Models;
using LN.Interfaces;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace WSE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly IPersonService _personService;
		private readonly IHttpClientFactory _httpClientFactory;

		public PersonController(IPersonService personService)
		{
			_personService = personService;
		}

		[HttpGet]
		public ActionResult<List<Person>> GetAllPersons()
		{
			var allPersons = _personService.GetAllPersons();
			return Ok(allPersons);
		}

		[HttpGet("{id}")]
		public ActionResult<Person> GetPerson(int id)
		{
			var person = _personService.GetPersonById(id);

			if (person == null)
			{
				return NotFound();
			}

			return Ok(person);
		}

		[HttpPost]
		public ActionResult<Person> AddPerson([FromBody] Person person)
		{
			if (person == null)
			{
				return BadRequest("Invalid data");
			}

			var addedPerson = _personService.AddPerson(person);

			return CreatedAtAction(nameof(GetPerson), new { id = addedPerson.Id }, addedPerson);
		}

		[HttpPut("{id}")]
		public IActionResult UpdatePerson(int id, [FromBody] Person person)
		{
			if (person == null)
			{
				return BadRequest("Invalid data");
			}

			var updated = _personService.UpdatePerson(person);

			if (!updated)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeletePerson(int id)
		{
			var deleted = _personService.DeletePerson(id);

			if (!deleted)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
