namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InQCategoriesController(InQCategoryService inQCategoryService) : ControllerBase
{
     [HttpPost]
     public async Task<ActionResult<InQCategoryDto>> CreateCategoryAsync([FromBody] CreateInQCategoryDto createCategoryDto)
     {
         var category = await inQCategoryService.CreateInQCategoryAsync(createCategoryDto);
         return this.Ok(category);
     }

     [HttpGet]
     public async Task<ActionResult<List<InQCategoryDto>>> GetAllCategoriesAsync()
    {
        var categories = await inQCategoryService.GetAllInQCategoryAsync();
        return this.Ok(categories);
    }

     [HttpPut("{CategoryId}")]
     public async Task<ActionResult<InQCategoryDto>> UpdateCategory(int categoryId, string newCategory)
    {
        await inQCategoryService.ChangeInQCategoryAsync(categoryId, newCategory);
        return this.Ok();
    }

     [HttpDelete("{CategoryId}")]
     public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        await inQCategoryService.DeleteCategory(categoryId);
        return this.Ok();
    }
}