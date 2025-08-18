namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/interview-questions/categories")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
     [HttpPost]
     public async Task<CategoryDto> CreateCategoryAsync([FromBody] CreateCategoryDto createCategoryDto)
     {
         var category = await categoryService.CreateCategoryAsync(createCategoryDto);
         return category;
     }

     [HttpGet]
     public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return categories;
    }

     [HttpPut("{categoryId:int}")]
     public async Task<string> UpdateCategoryAsync(int categoryId, string newCategory)
    {
        await categoryService.ChangeCategoryAsync(categoryId, newCategory);

        return $"{categoryId} {newCategory}";
    }

     [HttpDelete("{categoryId:int}")]
     public async Task<string> DeleteCategoryAsync(int categoryId)
    {
        await categoryService.DeleteCategoryAsync(categoryId);
        return $"Удалена категория {categoryId}";
    }
}