using Alex_Chasi_Person_Test_Api.Data;
using Alex_Chasi_Person_Test_Api.Models.Entities;
using Alex_Chasi_Person_Test_Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alex_Chasi_Person_Test_Api.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            person.UpdatedAt = DateTime.UtcNow;
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            if (person == null)
                return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Persons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Persons
                .AnyAsync(p => p.Email == email && (!excludeId.HasValue || p.Id != excludeId.Value));
        }
    }
}