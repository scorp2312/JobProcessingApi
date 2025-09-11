namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository(ApplicationDbContext context) : RepositoryBase<UserEntity, Guid>(context), IUserRepository
{
    private readonly ApplicationDbContext context = context;

    public async Task<PaginatedList<UserEntity>> FindAsync(string? login, int pageIndex, int pageSize)
    {
        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = this.context.Users
            .AsQueryable();

        if (login != null)
        {
            query = query.Where(u => u.Login == login);
        }

        int totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(q => q.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<UserEntity>(items, totalItems, pageIndex, pageSize);
    }
}