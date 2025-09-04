namespace InterviewGuide.Controllers;

using InterviewGuide.Application.Models;
using InterviewGuide.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/questions/{questionId:guid}/comments")]

public class CommentsController(QuestionService questionService) : ControllerBase
{
     [HttpGet]
     public async Task<List<CommentDto>> GetAllCommentsByQuestionAsync([FromRoute] Guid questionId)
    {
        return await questionService.GetAllCommentsByQuestionAsync(questionId);
    }

     [HttpPost]
     public async Task<CommentDto> CreateCommentAsync([FromRoute] Guid questionId, [FromBody] CreateCommentDto commentDto)
    {
        return await questionService.CreateCommentAsync(commentDto, questionId);
    }

     [HttpDelete("{commentId:guid}")]
     public async Task<IActionResult> DeleteCommentAsync([FromRoute] Guid commentId)
     {
         var deleted = await questionService.DeleteCommentAsync(commentId);
         return deleted ? this.Ok() : this.NotFound();
     }
}