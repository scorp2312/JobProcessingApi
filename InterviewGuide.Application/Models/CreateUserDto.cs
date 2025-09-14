namespace InterviewGuide.Application.Models;

using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    [MaxLength(25)]
    [RegularExpression(
        @"^(?=.*[a-zA-Z0-9])[a-zA-Z0-9]*$",
        ErrorMessage = "Логин должен содержать только английские буквы и цифры, и не может быть пустым")]
    public required string Login { get; init; }

    [Required]
    public required string Password { get; init; }

    public required List<int> RoleIds { get; init; }
}