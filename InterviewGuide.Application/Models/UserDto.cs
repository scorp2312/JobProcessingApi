namespace InterviewGuide.Application.Models;

public class UserDto
{
    public required Guid Id { get; init; }

    public required string Login { get; init; }

    public required string PasswordHash { get; init; }

    public required List<int> RoleIds { get; init; }
}
