using Alex_Chasi_Person_Test_Api.DTOs;
using Alex_Chasi_Person_Test_Api.Models.Entities;
using Alex_Chasi_Person_Test_Api.Repositories.Interfaces;
using Alex_Chasi_Person_Test_Api.Services.Interfaces;

namespace Alex_Chasi_Person_Test_Api.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
        {
            var persons = await _repository.GetAllAsync();
            return persons.Select(MapToDto);
        }

        public async Task<PersonDto?> GetPersonByIdAsync(int id)
        {
            var person = await _repository.GetByIdAsync(id);
            return person != null ? MapToDto(person) : null;
        }

        public async Task<PersonDto> CreatePersonAsync(CreatePersonDto createPersonDto)
        {
            // Validar que el email no exista
            if (await _repository.EmailExistsAsync(createPersonDto.Email))
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado");
            }

            var person = new Person
            {
                Name = createPersonDto.Name,
                Email = createPersonDto.Email,
                Age = createPersonDto.Age,
                Address = createPersonDto.Address,
                PhoneNumber = createPersonDto.PhoneNumber,
                City = createPersonDto.City,
                Country = createPersonDto.Country
            };

            var createdPerson = await _repository.CreateAsync(person);
            return MapToDto(createdPerson);
        }

        public async Task<PersonDto> UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto)
        {
            var person = await _repository.GetByIdAsync(id);
            if (person == null)
            {
                throw new KeyNotFoundException($"No se encontró la persona con ID {id}");
            }

            // Validar que el email no exista en otra persona
            if (await _repository.EmailExistsAsync(updatePersonDto.Email, id))
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado por otra persona");
            }

            person.Name = updatePersonDto.Name;
            person.Email = updatePersonDto.Email;
            person.Age = updatePersonDto.Age;
            person.Address = updatePersonDto.Address;
            person.PhoneNumber = updatePersonDto.PhoneNumber;
            person.City = updatePersonDto.City;
            person.Country = updatePersonDto.Country;

            var updatedPerson = await _repository.UpdateAsync(person);
            return MapToDto(updatedPerson);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static PersonDto MapToDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Age = person.Age,
                Address = person.Address,
                PhoneNumber = person.PhoneNumber,
                City = person.City,
                Country = person.Country,
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt
            };
        }
    }
}