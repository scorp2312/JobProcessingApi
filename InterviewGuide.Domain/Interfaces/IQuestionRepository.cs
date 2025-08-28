namespace InterviewGuide.Domain.Interfaces;

using InterviewGuide.Domain.Entities;

public interface IQuestionRepository : IRepository<QuestionEntity, Guid>
{
    Task<PaginatedList<QuestionEntity>> FindAndPaginateQuestionsAsync(int? categoryId, int pageIndex, int pageSize);
}