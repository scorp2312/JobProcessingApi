namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
     [HttpPost]
     public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CreateCategoryDto createCategoryDto)
     {
         var category = await categoryService.CreateInQCategoryAsync(createCategoryDto);
         return this.Ok(category);
     }

     [HttpGet]
     public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await categoryService.GetAllInQCategoryAsync();
        return this.Ok(categories);
    }

     [HttpPut("{categoryId}")]
     public async Task<ActionResult<CategoryDto>> UpdateCategory(int categoryId, string newCategory)
    {
        await categoryService.ChangeInQCategoryAsync(categoryId, newCategory);
        return this.Ok();
    }

     [HttpDelete("{categoryId}")]
     public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        await categoryService.DeleteCategory(categoryId);
        return this.Ok();
    }
}