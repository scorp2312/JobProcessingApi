namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Models;
using JobCreator.Services;
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
        var questions = await questionService
            .FindAndPaginateQuestionsAsync(
            categoryId,
            pageIndex,
            pageSize);

        return questions;
    }

    [HttpPost("Create")]
    public async Task<QuestionDto> CreateQuestionAsync([FromBody] CreateQuestionDto createQuestionDto)
    {
        var question = await questionService.CreateQuestionAsync(createQuestionDto);
        return question;
    }

    [HttpGet]
    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await questionService.GetAllQuestionsAsync();
        return questions;
    }

    [HttpPut("{id:guid}")]
    public async Task<QuestionDto?> UpdateQuestionAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateQuestionDto data)
    {
        var changedQuestion = await questionService.UpdateQuestionAsync(id, data);
        if (changedQuestion == null)
        {
            throw new ArgumentException($"Вопрос с Id: {id}. не найден");
        }

        return changedQuestion;
    }

    [HttpDelete("{id:guid}")]
    public async Task<string> DeleteQuestionAsync(Guid id)
    {
        var questionToDelete = await questionService.FindQuestionByIdAsync(id);
        if (questionToDelete == null)
        {
            return "Question not found";
        }

        await questionService.DeleteQuestionAsync(id);
        return $"Удален вопрос: {id} {questionToDelete.QuestionText}";
    }
}
