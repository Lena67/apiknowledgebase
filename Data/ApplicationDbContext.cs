using Microsoft.EntityFrameworkCore;
using knowledgebaseapi.Model;

namespace knowledgebaseapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notebook> Notebooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notebook>()
                .HasIndex(n => n.Name)
                .IsUnique(false);
        }
    }
}
