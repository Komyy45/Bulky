using System.Text.Json;
using Bulky.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bulky.Persistence.Data;

public class DbContextInitializer(ApplicationDbContext dbContext)
{
    public async Task SeedAsync()
    {
        if (!dbContext.Categories.Any())
        {
            var data = File.ReadAllText("../Bulky.Persistence/Data/Seeds/Categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(data);
            
            await dbContext.AddRangeAsync(categories!);
        }

        if (!dbContext.Products.Any())
        {
            var data = File.ReadAllText("../Bulky.Persistence/Data/Seeds/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(data);
            
            await dbContext.AddRangeAsync(products!);
        }
        
        await dbContext.SaveChangesAsync();
    }
}