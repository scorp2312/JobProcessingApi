namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Messages.Events;

public class InQService(
    ApplicationDbContext context)
{
    public async Task<InQuestionDto> CreateQuestion(CreateInQuestionDto createInQuestionDto)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == createInQuestionDto.CategoryId);

        if (category == null)
        {
            throw new ArgumentException($"CategoryName with ID {createInQuestionDto.CategoryId} not found");
        }

        var question = new InQuestion
            {
                Id = Guid.NewGuid(),
                Question = createInQuestionDto.Question,
                Answer = createInQuestionDto.Answer,
                Category = category,
            };

        context.InterviewQuestions.Add(question);
        await context.SaveChangesAsync();

        return MapToDto(question);
    }

    public async Task<List<InQuestionDto>> GetAllQuestions()
    {
        var questions = await context.InterviewQuestions.OrderBy(q => q.Id).ToListAsync();
        return questions.Select(MapToDto).ToList();
    }

    public async Task<InQuestionDto?> FindQuestionById(Guid id)
    {
        var question = await context.InterviewQuestions.FirstOrDefaultAsync(q => q.Id == id);
        if (question == null)
        {
            return null;
        }

        return MapToDto(question);
    }

    public async Task<InQuestionDto?> DeleteQuestion(Guid id)
    {
        var questionToDelete = await context.InterviewQuestions.FirstOrDefaultAsync(q => q.Id == id);
        if (questionToDelete == null)
        {
            return null;
        }

        context.InterviewQuestions.Remove(questionToDelete);
        await context.SaveChangesAsync();
        return MapToDto(questionToDelete);
    }

    public async Task<PaginatedList<InQuestionDto>> FindAndPaginateQuestions(int categoryId, int pageIndex = 1, int pageSize = 20)
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
            bool categoryExists = await context.Categories
                .AnyAsync(c => c.CategoryId == categoryId);

            query = query.Where(q => q.Category.CategoryId == categoryId);
        }

        int totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(q => q.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(question => new InQuestionDto
            {
                Id = question.Id,
                Question = question.Question,
                Answer = question.Answer,
                Category = new InQCategoryDto
                {
                    CategoryId = question.Category.CategoryId,
                    CategoryName = question.Category.CategoryName,
                },
            })
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<InQuestionDto>(items, totalItems, pageIndex, pageSize);
    }

    public async Task<InQuestionDto?> UpdateQuestion(Guid id, string? newQuestion, string? newAnswer, InQCategory? newCategory)
    {
        var questionToChange = await context.InterviewQuestions.FirstOrDefaultAsync(q => q.Id == id);
        if (questionToChange == null)
        {
            return null;
        }

        if (newQuestion != null)
        {
            questionToChange.Question = newQuestion;
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
        return MapToDto(questionToChange);
    }

    private static InQuestionDto MapToDto(InQuestion question)
    {
        return new InQuestionDto
        {
            Id = question.Id,
            Question = question.Question,
            Answer = question.Answer,
            Category = new InQCategoryDto
            {
                CategoryId = question.Category.CategoryId,
                CategoryName = question.Category.CategoryName,
            },
        };
    }
}