namespace InterviewGuide.Infrastructure.Data;

using InterviewGuide.Domain.Entities;

public static class DataSeeder
{
    public static async Task SeedDataAsync(ApplicationDbContext context)
    {
        if (context.Categories.Any() && context.Roles.Any())
        {
            return;
        }

        if (!context.Categories.Any())
        {
            var categories = new List<CategoryEntity>
                    {
                        new CategoryEntity { CategoryName = "C#", Id = 1 },
                        new CategoryEntity { CategoryName = "БД", Id = 2 },
                        new CategoryEntity { CategoryName = "Общее", Id = 3 },
                    };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!context.Roles.Any())
        {
            var roles = new List<RoleEntity>
            {
                new RoleEntity { Name = "Admin", Id = 1 },
                new RoleEntity { Name = "User", Id = 2 },
            };
            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }
    }
}