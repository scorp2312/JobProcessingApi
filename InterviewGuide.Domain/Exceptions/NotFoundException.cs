namespace InterviewGuide.Domain.Exceptions;

public class NotFoundException<TId>(TId id) : Exception($"Entity with id {id} was not found");