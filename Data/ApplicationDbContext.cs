using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> students { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        
        public DbSet<Faculty> faculties { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
