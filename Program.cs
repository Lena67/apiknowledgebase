using knowledgebaseapi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// --- Database Configuration (Uses the Connection String from appsettings) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Read it here
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)); // Use it here to configure the DbContext

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


    // Automatically apply database migrations and Seed data on startup in Development
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            // Apply any pending database migrations
            dbContext.Database.Migrate();

            // Seed the database with test data
            DataSeeder.SeedData(dbContext); // <-- Call your seeder here!

        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating, seeding, or initializing the database.");
            // Optionally re-throw the exception or handle differently
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
