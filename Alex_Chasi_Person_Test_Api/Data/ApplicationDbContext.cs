using Alex_Chasi_Person_Test_Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alex_Chasi_Person_Test_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional de la entidad Person
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Datos iniciales de prueba
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Name = "Juan Pérez",
                    Email = "juan.perez@example.com",
                    Age = 30,
                    Address = "Calle Principal 123",
                    PhoneNumber = "+593991234567",
                    City = "Quito",
                    Country = "Ecuador",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 2,
                    Name = "María García",
                    Email = "maria.garcia@example.com",
                    Age = 25,
                    Address = "Av. Amazonas 456",
                    PhoneNumber = "+593987654321",
                    City = "Guayaquil",
                    Country = "Ecuador",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 3,
                    Name = "Carlos Rodríguez",
                    Email = "carlos.rodriguez@example.com",
                    Age = 35,
                    Address = "Calle Sucre 789",
                    PhoneNumber = "+593971112222",
                    City = "Cuenca",
                    Country = "Ecuador",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}