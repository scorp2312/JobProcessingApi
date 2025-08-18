namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Models;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InQuestionController(InQService inQService) : ControllerBase
{
    [HttpGet("Find")]
    public async Task<IActionResult> FindQuestions(
        [FromQuery] int categoryId,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var questions = await inQService.FindAndPaginateQuestions(
                                                                                            categoryId,
                                                                                            pageIndex,
                                                                                            pageSize);

        return this.Ok(questions);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateInQuestionDto createInQuestionDto)
    {
        var question = await inQService.CreateQuestion(createInQuestionDto);
        return this.Ok(question);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQuestions()
    {
        var questions = await inQService.GetAllQuestions();
        return this.Ok(questions);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InQuestionDto>> UpdateQuestion(
        Guid id,
        string? newquestion,
        string? newanswer,
        InQCategory? newCategory)
    {
        var changedQuestion = await inQService.UpdateQuestion(id, newquestion, newanswer, newCategory);
        if (changedQuestion == null)
        {
            return this.NotFound();
        }

        return this.Ok(changedQuestion);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<InQuestionDto>> DeleteQuestion(Guid id)
    {
        var questionToDelete = await inQService.FindQuestionById(id);
        if (questionToDelete == null)
        {
            return this.NotFound();
        }

        await inQService.DeleteQuestion(id);
        return this.Ok($"Удален вопрос: {questionToDelete.Id} {questionToDelete.Question} ");
    }
}
