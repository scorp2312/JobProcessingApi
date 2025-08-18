namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using Microsoft.EntityFrameworkCore;

public class QuestionService(
    ApplicationDbContext context)
{
    public async Task<QuestionDto> CreateQuestion(CreateQuestionDto createQuestionDto)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == createQuestionDto.CategoryId);

        if (category == null)
        {
            throw new ArgumentException($"CategoryName with ID {createQuestionDto.CategoryId} not found");
        }

        var question = new Question
            {
                Id = Guid.NewGuid(),
                QuestionText = createQuestionDto.QuestionText,
                Answer = createQuestionDto.Answer,
                Category = category,
            };

        context.InterviewQuestions.Add(question);
        await context.SaveChangesAsync();

        return MapToDto(question);
    }

    public async Task<List<QuestionDto>> GetAllQuestions()
    {
        var questions = await context.InterviewQuestions
            .Include(q => q.Category)
            .OrderByDescending(q => q.Id)
            .ToListAsync();

        return questions.Select(MapToDto).ToList();
    }

    public async Task<QuestionDto?> FindQuestionById(Guid id)
    {
        var question = await context.InterviewQuestions.FirstOrDefaultAsync(q => q.Id == id);
        if (question == null)
        {
            return null;
        }

        return MapToDto(question);
    }

    public async Task<QuestionDto?> DeleteQuestion(Guid id)
    {
        var questionToDelete = await context.InterviewQuestions
            .Include(q => q.Category)
            .FirstOrDefaultAsync(q => q.Id == id);
        if (questionToDelete == null)
        {
            return null;
        }

        context.InterviewQuestions.Remove(questionToDelete);
        await context.SaveChangesAsync();
        return MapToDto(questionToDelete);
    }

    public async Task<PaginatedList<QuestionDto>> FindAndPaginateQuestions(int categoryId, int pageIndex = 1, int pageSize = 20)
    {
        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = context.InterviewQuestions
            .Include(q => q.Category)
            .AsQueryable();

        if (categoryId != 0)
        {
            query = query.Where(q => q.Category.CategoryId == categoryId);
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
                Category = new CategoryDto
                {
                    CategoryId = question.Category.CategoryId,
                    CategoryName = question.Category.CategoryName,
                },
            })
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<QuestionDto>(items, totalItems, pageIndex, pageSize);
    }

    public async Task<QuestionDto?> UpdateQuestion(Guid id, string? newQuestion, string? newAnswer, Category? newCategory)
    {
        var questionToChange = await context.InterviewQuestions.FirstOrDefaultAsync(q => q.Id == id);
        if (questionToChange == null)
        {
            return null;
        }

        if (newQuestion != null)
        {
            questionToChange.QuestionText = newQuestion;
        }

        if (newAnswer != null)
        {
            questionToChange.Answer = newAnswer;
        }

        if (newCategory != null)
        {
            questionToChange.Category = newCategory;
        }

        context.InterviewQuestions.Update(questionToChange);
        await context.SaveChangesAsync();
        return MapToDto(questionToChange);
    }

    private static QuestionDto MapToDto(Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            Answer = question.Answer,
            Category = question.Category == null
                ? null
                : new CategoryDto
            {
                CategoryId = question.Category.CategoryId,
                CategoryName = question.Category.CategoryName,
            },
        };
    }
}