namespace InterviewGuide.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Login { get; set; }

    public required string PasswordHash { get; set; }

    public required List<RoleEntity> Roles { get; set; }
}