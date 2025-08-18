namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class QuestionService(
    ApplicationDbContext context)
{
    public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto createQuestionDto)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == createQuestionDto.CategoryId);

        if (category == null)
        {
            throw new ArgumentException($"Provided category not found. Category Id: {createQuestionDto.CategoryId}");
        }

        var question = new QuestionEntity
        {
            Id = Guid.NewGuid(),
            QuestionText = createQuestionDto.QuestionText,
            Answer = createQuestionDto.Answer,
            CategoryEntity = category,
        };

        context.InterviewQuestions.Add(question);
        await context.SaveChangesAsync();

        return MapToDto(question);
    }

    public async Task<List<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await context.InterviewQuestions
            .Include(q => q.CategoryEntity)
            .OrderByDescending(q => q.Id)
            .AsNoTracking()
            .ToListAsync();

        return questions.Select(MapToDto).ToList();
    }

    public async Task<QuestionDto?> FindQuestionByIdAsync(Guid id)
    {
        var question = await context.InterviewQuestions
            .Include(q => q.CategoryEntity)
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
        if (question == null)
        {
            return null;
        }

        return MapToDto(question);
    }

    public async Task<bool> DeleteQuestionAsync(Guid id)
    {
        var questionToDelete = await context.InterviewQuestions
            .Include(q => q.CategoryEntity)
            .FirstOrDefaultAsync(q => q.Id == id);
        if (questionToDelete == null)
        {
            return false;
        }

        context.InterviewQuestions.Remove(questionToDelete);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<PaginatedList<QuestionDto>> FindAndPaginateQuestionsAsync(int categoryId, int pageIndex = 1, int pageSize = 20)
    {
        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = context.InterviewQuestions
            .Include(q => q.CategoryEntity)
            .AsQueryable();

        if (categoryId != 0)
        {
            query = query.Where(q => q.CategoryEntity.Id == categoryId);
        }

        int totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(q => q.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(question => new QuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                Answer = question.Answer,
                CategoryId = question.CategoryEntity.Id,
            })
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<QuestionDto>(items, totalItems, pageIndex, pageSize);
    }

    public async Task<bool> UpdateQuestionAsync(Guid id, UpdateQuestionDto data)
    {
        var questionToChange = await context.InterviewQuestions
            .Include(q => q.CategoryEntity)
            .FirstOrDefaultAsync(q => q.Id == id);
        if (questionToChange == null)
        {
            return false;
        }

        questionToChange.QuestionText = data.NewQuestion;
        questionToChange.Answer = data.NewAnswer;
        if (data.CategoryId > 0)
        {
            var category = await context.Categories.FindAsync(data.CategoryId);
            if (category != null)
            {
                questionToChange.CategoryEntity = category;
            }
        }

        context.InterviewQuestions.Update(questionToChange);
        await context.SaveChangesAsync();
        return true;
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