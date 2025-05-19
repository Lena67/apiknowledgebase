using knowledgebaseapi.Model;

namespace knowledgebaseapi.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            // Check if the database already contains any Notebooks
            // This prevents adding duplicate data every time you run the app
            if (context.Notebooks.Any())
            {
                // Database has already been seeded
                return;
            }

            // Add some sample notebooks
            var notebooks = new Notebook[]
            {
                new Notebook
                {
                    NotebookId = Guid.NewGuid(),
                    Name = "Getting Started with .NET 8",
                    Content = "# .NET 8 Basics\nThis notebook covers the fundamentals of .NET 8.",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    IsFavorite = true
                },
                 new Notebook
                {
                    NotebookId = Guid.NewGuid(),
                    Name = "SQLite and EF Core Guide",
                    Content = "# SQLite & EF Core\nNotes on setting up EF Core with SQLite.",
                    CreatedDate = DateTime.UtcNow.AddDays(-1), // Example of different dates
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                    IsFavorite = false
                },
                new Notebook
                {
                    NotebookId = Guid.NewGuid(),
                    Name = "Markdown Syntax Cheatsheet",
                    Content = "# Markdown\n- **Bold**\n- *Italic*",
                    CreatedDate = DateTime.UtcNow.AddHours(-5),
                    LastModifiedDate = DateTime.UtcNow.AddHours(-5),
                    IsFavorite = false
                }
            };

            // Add the notebooks to the DbContext
            context.Notebooks.AddRange(notebooks);

            // Save changes to the database
            context.SaveChanges();

            // Optional: Log that data was seeded
            Console.WriteLine("Database seeded with initial data.");
        }
    }
}

