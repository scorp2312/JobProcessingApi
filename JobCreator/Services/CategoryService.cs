namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService(
    ApplicationDbContext context)
{
    public async Task<CategoryDto> CreateInQCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var newId = context.Categories.OrderByDescending(e => e.CategoryId).First();
        var category = new Category
        {
            CategoryId = newId.CategoryId + 1,
            CategoryName = createCategoryDto.CategoryName,
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<List<CategoryDto>> GetAllInQCategoryAsync()
    {
        var categories = await context.Categories
            .OrderBy(c => c.CategoryId)
            .ToListAsync();
        return categories.Select(MapToDto).ToList();
    }

    public async Task ChangeInQCategoryAsync(int categoryId, string newCategory)
    {
        var category = await context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            return;
        }

        category.CategoryName = newCategory;
        await context.SaveChangesAsync();
        MapToDto(category);
    }

    public async Task<CategoryDto?> FindCategoryById(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        return category == null ? null : MapToDto(category);
    }

    public async Task DeleteCategory(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category == null)
        {
            return;
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        MapToDto(category);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
        };
    }
}