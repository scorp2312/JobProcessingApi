namespace InterviewGuide.Domain.Interfaces;

using InterviewGuide.Domain.Entities;

public interface IUserRepository : IRepository<UserEntity, Guid>
{
    Task<PaginatedList<UserEntity>> FindAsync(string? login, int pageNumber, int pageSize);
}