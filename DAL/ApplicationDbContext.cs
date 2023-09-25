using Models;

using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

		// Define las DbSet para tus entidades aquí.
		// El nombre de la variable DbSet tiene que ser igual al de la base de datos
		public DbSet<Person> Persons { get; set; }
         
        // Puedes agregar configuraciones adicionales para tus entidades en el método OnModelCreating si lo necesitas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales de entidades aquí
        }
    }
}
