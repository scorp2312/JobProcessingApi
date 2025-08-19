namespace InterviewGuide.Services;

using InterviewGuide.Data;
using InterviewGuide.DTOs;
using InterviewGuide.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService(
    ApplicationDbContext context)
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = new CategoryEntity
        {
            CategoryName = createCategoryDto.CategoryName,
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await context.Categories
            .OrderBy(c => c.Id)
            .AsNoTracking()
            .ToListAsync();
        return categories.Select(MapToDto).ToList();
    }

    public async Task<bool> UpdateCategoryAsync(int id, string newCategory)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            return false;
        }

        category.CategoryName = newCategory;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<CategoryDto?> FindCategoryById(int id)
    {
        var category = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return category == null ? null : MapToDto(category);
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null)
        {
            return false;
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }

    private static CategoryDto MapToDto(CategoryEntity categoryEntity)
    {
        return new CategoryDto
        {
            Id = categoryEntity.Id,
            CategoryName = categoryEntity.CategoryName,
        };
    }
}