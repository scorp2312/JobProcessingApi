namespace InterviewGuide.Domain.Interfaces;

using InterviewGuide.Domain.Entities;

public interface ICommentRepository : IRepository<CommentEntity, Guid>
{
}