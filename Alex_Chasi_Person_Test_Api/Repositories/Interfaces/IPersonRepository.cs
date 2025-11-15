using Alex_Chasi_Person_Test_Api.Models.Entities;

namespace Alex_Chasi_Person_Test_Api.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}