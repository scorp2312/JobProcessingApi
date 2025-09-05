namespace InterviewGuide.Domain.Exceptions;

public class NotFoundException(string? id) : Exception($"Entity with id {id} was not found");