namespace InterviewGuide.Infrastructure.Repositories;

using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Interfaces;
using InterviewGuide.Infrastructure.Data;

public class CategoryRepository(ApplicationDbContext context) : RepositoryBase<CategoryEntity, int>(context), ICategoryRepository
{
}