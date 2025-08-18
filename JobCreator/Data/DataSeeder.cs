namespace JobCreator.Data;

using JobCreator.Models;

public static class DataSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        var categories = new List<InQCategory>
        {
            new InQCategory { CategoryName = "C#", CategoryId = 1 },
            new InQCategory { CategoryName = "БД", CategoryId = 2 },
            new InQCategory { CategoryName = "Общее", CategoryId = 3 },
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}