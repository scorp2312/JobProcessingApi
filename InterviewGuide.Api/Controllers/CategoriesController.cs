namespace InterviewGuide.Controllers;

using InterviewGuide.Application.Models;
using InterviewGuide.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/interview-questions/categories")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
     [HttpPost]
     public async Task<CategoryDto> CreateCategoryAsync([FromBody] CreateCategoryDto request)
     {
         var category = await categoryService.CreateCategoryAsync(request);
         return category;
     }

     [HttpGet]
     public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return categories;
    }

     [HttpPut("{categoryId:int}")]
     public async Task<IActionResult> UpdateCategoryAsync(int categoryId, string newCategoryName)
    {
        var updated = await categoryService.UpdateCategoryAsync(categoryId, newCategoryName);
        return updated ? this.Ok() : this.NotFound();
    }

     [HttpDelete("{categoryId:int}")]
     public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
    {
        var deleted = await categoryService.DeleteCategoryAsync(categoryId);
        return deleted ? this.Ok() : this.NotFound();
    }
}