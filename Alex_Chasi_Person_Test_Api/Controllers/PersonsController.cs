using Alex_Chasi_Person_Test_Api.DTOs;
using Alex_Chasi_Person_Test_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alex_Chasi_Person_Test_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        // GET: api/persons
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllPersons()
        {
            try
            {
                var persons = await _personService.GetAllPersonsAsync();
                return Ok(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las personas");
                return StatusCode(500, new { message = "Error al obtener las personas", error = ex.Message });
            }
        }

        // GET: api/persons/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDto>> GetPerson(int id)
        {
            try
            {
                var person = await _personService.GetPersonByIdAsync(id);

                if (person == null)
                {
                    return NotFound(new { message = $"No se encontró la persona con ID {id}" });
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la persona con ID {id}");
                return StatusCode(500, new { message = "Error al obtener la persona", error = ex.Message });
            }
        }

        // POST: api/persons
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] CreatePersonDto createPersonDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var person = await _personService.CreatePersonAsync(createPersonDto);
                return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva persona");
                return StatusCode(500, new { message = "Error al crear la persona", error = ex.Message });
            }
        }

        // PUT: api/persons/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDto>> UpdatePerson(int id, [FromBody] UpdatePersonDto updatePersonDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var person = await _personService.UpdatePersonAsync(id, updatePersonDto);
                return Ok(person);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la persona con ID {id}");
                return StatusCode(500, new { message = "Error al actualizar la persona", error = ex.Message });
            }
        }

        // DELETE: api/persons/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personService.DeletePersonAsync(id);

                if (!result)
                {
                    return NotFound(new { message = $"No se encontró la persona con ID {id}" });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la persona con ID {id}");
                return StatusCode(500, new { message = "Error al eliminar la persona", error = ex.Message });
            }
        }
    }
}