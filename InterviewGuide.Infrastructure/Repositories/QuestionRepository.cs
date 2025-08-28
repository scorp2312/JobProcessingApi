namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class QuestionRepository(ApplicationDbContext context) : RepositoryBase<QuestionEntity, Guid>(context), IQuestionRepository
{
    private readonly ApplicationDbContext context = context;

    public override async Task<QuestionEntity> AddAsync(QuestionEntity entity)
    {
        await this.dbSet.AddAsync(entity);
        await this.context.SaveChangesAsync();
        return entity;
    }

    public override async Task<List<QuestionEntity>> GetAllAsync()
    {
        return await this.dbSet
            .Include(q => q.CategoryEntity)
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<QuestionEntity?> GetAsync(Guid id)
    {
        return await this.dbSet
            .Include(q => q.CategoryEntity)
            .FirstOrDefaultAsync(q => q.Id == id) ?? throw new ApplicationException($"Вопроса с Id: {id}. не существует.");
    }

    public override async Task<bool> UpdateAsync(QuestionEntity entity)
    {
        return await this.context.SaveChangesAsync() > 0;
    }

    public async Task<PaginatedList<QuestionEntity>> FindAndPaginateQuestionsAsync(
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