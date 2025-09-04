namespace InterviewGuide.Controllers;

using InterviewGuide.Application.Models;
using InterviewGuide.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/questions/categories")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
     [HttpPost]
     public async Task<CategoryDto> CreateCategoryAsync([FromBody] CreateCategoryDto categoryDto)
     {
         var category = await categoryService.CreateCategoryAsync(categoryDto);
         return category;
     }

     [HttpGet]
     public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return categories;
    }

     [HttpPut("{id:int}")]
     public async Task<IActionResult> UpdateCategoryAsync(int id, string newCategoryName)
    {
        var updated = await categoryService.UpdateCategoryAsync(id, newCategoryName);
        return updated ? this.Ok() : this.NotFound();
    }

     [HttpDelete("{categoryId:int}")]
     public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
    {
        var deleted = await categoryService.DeleteCategoryAsync(categoryId);
        return deleted ? this.Ok() : this.NotFound();
    }
}