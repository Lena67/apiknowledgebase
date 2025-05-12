using APIKnowledgeBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace APIKnowledgeBase.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor needed for dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Represents the 'Notebooks' table in the database
        public DbSet<Notebook> Notebooks { get; set; } = null!;

        // This method is called by EF Core to configure the model and database mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Configuration for the Notebook entity ---
            // Create a database index on the 'Name' column. 
            // This is VITAL for making the autocomplete suggestion query fast.
            modelBuilder.Entity<Notebook>()
                .HasIndex(n => n.Name)
                .IsUnique(false); // Set to true if you want to enforce unique notebook names

            // Add any other specific configurations for your model here if needed
        }
    }
}
