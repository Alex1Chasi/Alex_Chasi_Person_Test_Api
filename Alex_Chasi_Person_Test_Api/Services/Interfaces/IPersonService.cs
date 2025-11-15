using Alex_Chasi_Person_Test_Api.DTOs;

namespace Alex_Chasi_Person_Test_Api.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllPersonsAsync();
        Task<PersonDto?> GetPersonByIdAsync(int id);
        Task<PersonDto> CreatePersonAsync(CreatePersonDto createPersonDto);
        Task<PersonDto> UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto);
        Task<bool> DeletePersonAsync(int id);
    }
}