namespace InterviewGuide.Application.Services;

using InterviewGuide.Application.Models;
using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;

public class CategoryService(IRepository<CategoryEntity, int> categoryRepository)
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var entity = new CategoryEntity()
        {
            CategoryName = categoryDto.CategoryName,
        };

        await categoryRepository.AddAsync(entity);
        return MapCategoryToDto(entity);
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(MapCategoryToDto).ToList();
    }

    public async Task<bool> UpdateCategoryAsync(int id, string newCategoryName)
    {
        var category = await categoryRepository.GetAsync(id)
            ?? throw new NotFoundException(id.ToString());

        category.CategoryName = newCategoryName;
        return await categoryRepository.UpdateAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.GetAsync(id) ?? throw new NotFoundException(id.ToString());
        return await categoryRepository.DeleteAsync(category);
    }

    private static CategoryDto MapCategoryToDto(CategoryEntity categoryEntity)
    {
        return new CategoryDto
        {
            Id = categoryEntity.Id,
            CategoryName = categoryEntity.CategoryName,
        };
    }
}