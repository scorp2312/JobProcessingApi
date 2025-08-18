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
    public async Task<IActionResult> FindQuestions(
        [FromQuery] int categoryId,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var questions = await questionService.FindAndPaginateQuestions(
                                                                                            categoryId,
                                                                                            pageIndex,
                                                                                            pageSize);

        return this.Ok(questions);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
    {
        var question = await questionService.CreateQuestion(createQuestionDto);
        return this.Ok(question);
    }

    [HttpGet]
    public async Task<ActionResult<List<QuestionDto>>> GetAllQuestions()
    {
        var questions = await questionService.GetAllQuestions();
        return this.Ok(questions);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<QuestionDto>> UpdateQuestion(
        Guid id,
        string? newquestion,
        string? newanswer,
        Category? newCategory)
    {
        var changedQuestion = await questionService.UpdateQuestion(id, newquestion, newanswer, newCategory);
        if (changedQuestion == null)
        {
            return this.NotFound();
        }

        return this.Ok(changedQuestion);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<QuestionDto>> DeleteQuestion(Guid id)
    {
        var questionToDelete = await questionService.FindQuestionById(id);
        if (questionToDelete == null)
        {
            return this.NotFound();
        }

        await questionService.DeleteQuestion(id);
        return this.Ok();
    }
}
