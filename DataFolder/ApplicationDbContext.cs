using Microsoft.EntityFrameworkCore;
using DemoCRUD.Models;


namespace DemoCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base DbContext class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSet to represent the "Students" table in the database
        public DbSet<Student> Students { get; set; }
    }
}
