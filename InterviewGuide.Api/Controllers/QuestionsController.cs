namespace InterviewGuide.Controllers;

using InterviewGuide.Application.Services;
using InterviewGuide.Domain.Entities;
using InterviewGuide.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController(QuestionService questionService) : ControllerBase
{
    [HttpGet("Find")]
    public async Task<PaginatedList<QuestionDto>> FindQuestionsAsync(
        [FromQuery] int categoryId,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var questions = await questionService.FindAndPaginateQuestionsAsync(
            categoryId, pageIndex, pageSize);

        return questions;
    }

    [HttpPost("Create")]
    public async Task<QuestionDto> CreateQuestionAsync([FromBody] CreateQuestionDto request)
    {
        return await questionService.CreateQuestionAsync(request);
    }

    [HttpGet]
    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        return await questionService.GetAllQuestionsAsync();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateQuestionDto data)
    {
        var changed = await questionService.UpdateQuestionAsync(id, data);

        return changed ? this.Ok() : this.NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteQuestionAsync(Guid id)
    {
        var deleted = await questionService.DeleteQuestionAsync(id);
        return deleted ? this.Ok() : this.NotFound();
    }
}
