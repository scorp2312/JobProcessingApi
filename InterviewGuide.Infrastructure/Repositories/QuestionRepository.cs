namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class QuestionRepository(ApplicationDbContext context) : RepositoryBase<QuestionEntity, Guid>(context), IQuestionRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<PaginatedList<QuestionEntity>> FindAsync(
        int? categoryId,
        int pageIndex,
        int pageSize)
    {
        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = this.context.InterviewQuestions
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
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<QuestionEntity>(items, totalItems, pageIndex, pageSize);
    }
}