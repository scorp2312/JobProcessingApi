namespace InterviewGuide.Application.Services;

using InterviewGuide.Application.Models;
using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;

public class QuestionService(
    IQuestionRepository questionRepository,
    IRepository<CategoryEntity, int> categoryRepository,
    IRepository<CommentEntity, Guid> commentRepository)
{
    public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto questionDto)
    {
        var category = await categoryRepository.GetAsync(questionDto.CategoryId)
            ?? throw new NotFoundException<int>(questionDto.CategoryId);

        var question = new QuestionEntity
        {
            QuestionText = questionDto.QuestionText,
            Answer = questionDto.Answer,
            CategoryEntity = category,
        };

        await questionRepository.AddAsync(question);
        return MapQuestionToDto(question);
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await questionRepository.GetAllAsync();
        return questions.Select(MapQuestionToDto).ToList();
    }

    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var question = await questionRepository.GetAsync(id)
            ?? throw new NotFoundException<Guid>(id);
        return await questionRepository.DeleteAsync(question);
    }

    public async Task<PaginatedList<QuestionDto>> FindAsync(int id, int pageIndex, int pageSize)
    {
        var paginatedEntities = await questionRepository.FindAsync(id, pageIndex, pageSize);

        var questionDtos = paginatedEntities.Items.Select(entity => new QuestionDto
        {
            Id = entity.Id,
            QuestionText = entity.QuestionText,
            Answer = entity.Answer,
            CategoryId = entity.CategoryEntity.Id,
        }).ToList();

        return new PaginatedList<QuestionDto>(
            questionDtos,
            paginatedEntities.TotalItems,
            paginatedEntities.PageIndex,
            paginatedEntities.TotalPages);
    }

    public async Task<bool> UpdateQuestionAsync(Guid id, UpdateQuestionDto questionDto)
    {
        var question = await questionRepository.GetAsync(id)
            ?? throw new NotFoundException<Guid>(id);

        if (questionDto.NewQuestion != null)
        {
            question.QuestionText = questionDto.NewQuestion;
        }

        if (questionDto.NewAnswer != null)
        {
            question.Answer = questionDto.NewAnswer;
        }

        if (questionDto.CategoryId != null)
        {
            question.CategoryEntity = await categoryRepository.GetAsync(questionDto.CategoryId.Value)
                ?? throw new NotFoundException<Guid>(id);
        }

        return await questionRepository.UpdateAsync(question);
    }

    public async Task<List<CommentDto>> GetAllCommentsByQuestionAsync(Guid id)
    {
        var question = await questionRepository.GetAsync(id) ?? throw new NotFoundException<Guid>(id);
        var comments = question.Comments.Select(MapCommentsToDto).ToList();
        return comments;
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto, Guid id)
    {
        var question = await questionRepository.GetAsync(id) ?? throw new NotFoundException<Guid>(id);
        var comment = new CommentEntity
        {
            Author = commentDto.Author,
            Content = commentDto.Content,
            Question = question,
            QuestionId = question.Id,
        };
        await commentRepository.AddAsync(comment);
        return MapCommentsToDto(comment);
    }

    public async Task<bool> DeleteCommentAsync(Guid commentId)
    {
        var comment = await commentRepository.GetAsync(commentId)
            ?? throw new NotFoundException<Guid>(commentId);
        return await commentRepository.DeleteAsync(comment);
    }

    private static QuestionDto MapQuestionToDto(QuestionEntity questionEntity)
    {
        return new QuestionDto
        {
            Id = questionEntity.Id,
            QuestionText = questionEntity.QuestionText,
            Answer = questionEntity.Answer,
            CategoryId = questionEntity.CategoryEntity.Id,
        };
    }

    private static CommentDto MapCommentsToDto(CommentEntity commentEntity)
    {
        return new CommentDto
        {
            Id = commentEntity.Id,
            Author = commentEntity.Author,
            Content = commentEntity.Content,
            Created = commentEntity.Created,
        };
    }
}