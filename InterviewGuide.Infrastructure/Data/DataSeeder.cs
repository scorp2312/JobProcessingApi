namespace InterviewGuide.Infrastructure.Data;

using InterviewGuide.Domain.Entities;

public static class DataSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        var categories = new List<CategoryEntity>
        {
            new CategoryEntity { CategoryName = "C#", Id = 1 },
            new CategoryEntity { CategoryName = "БД", Id = 2 },
            new CategoryEntity { CategoryName = "Общее", Id = 3 },
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}