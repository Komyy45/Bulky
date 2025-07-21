using System.Text.Json;
using Bulky.Core.Entities;

namespace Bulky.Persistence.Data;

public class DbContextInitializer(ApplicationDbContext dbContext)
{
    public async Task SeedAsync()
    {
        if (!dbContext.Categories.Any())
        {
            var data = File.ReadAllText("../Bulky.Persistence/Data/Seeds/Categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(data);
            
            await dbContext.Categories.AddRangeAsync(categories!);
        }

        await dbContext.SaveChangesAsync();
    }
}