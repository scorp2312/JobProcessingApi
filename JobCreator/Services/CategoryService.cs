namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService(
    ApplicationDbContext context)
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var newId = context.Categories.OrderByDescending(e => e.Id).First();
        var category = new Category
        {
            Id = newId.Id + 1,
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

    public async Task ChangeCategoryAsync(int id, string newCategory)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
        {
            throw new ArgumentException($"Категория с айди: {id} не найдена");
        }

        category.CategoryName = newCategory;
        await context.SaveChangesAsync();
        MapToDto(category);
    }

    public async Task<CategoryDto?> FindCategoryById(int id)
    {
        var category = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return category == null ? null : MapToDto(category);
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category == null)
        {
            throw new ArgumentException($"Категория с айди: {categoryId} не найдена");
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        MapToDto(category);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
        };
    }
}