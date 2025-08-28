namespace InterviewGuide.Application.Services;

using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.DTOs;

public class QuestionService(IQuestionRepository questionRepository, ICategoryRepository categoryRepository)
{
    public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto questionDto)
    {
        var category = await categoryRepository
            .GetAsync(questionDto.CategoryId)
                        ?? throw new Exception("Category not found");

        var question = new QuestionEntity
        {
            QuestionText = questionDto.QuestionText,
            Answer = questionDto.Answer,
            CategoryEntity = category,
        };

        await questionRepository.AddAsync(question);
        return MapToDto(question);
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await questionRepository.GetAllAsync();
        return questions.Select(MapToDto).ToList();
    }

    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var question = await questionRepository
            .GetAsync(id)
                       ?? throw new Exception("question not found");
        return await questionRepository.DeleteAsync(question);
    }

    public async Task<PaginatedList<QuestionDto>> FindAndPaginateQuestionsAsync(int id, int pageIndex, int pageSize)
    {
        var paginatedEntities = await questionRepository.FindAndPaginateQuestionsAsync(
            id,
            pageIndex,
            pageSize);

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
        var question = await questionRepository
            .GetAsync(id)
                       ?? throw new Exception("question not found");

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
            question.CategoryEntity = await categoryRepository
                .GetAsync(questionDto.CategoryId.Value)
                                      ?? throw new Exception("Category not found");
        }

        return await questionRepository.UpdateAsync(question);
    }

    private static QuestionDto MapToDto(QuestionEntity questionEntity)
    {
        return new QuestionDto
        {
            Id = questionEntity.Id,
            QuestionText = questionEntity.QuestionText,
            Answer = questionEntity.Answer,
            CategoryId = questionEntity.CategoryEntity.Id,
        };
    }
}