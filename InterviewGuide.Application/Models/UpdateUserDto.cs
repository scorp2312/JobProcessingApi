namespace InterviewGuide.Application.Models;

using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
{
    [MaxLength(25)]
    [RegularExpression(
        @"^(?=.*[a-zA-Z0-9])[a-zA-Z0-9]*$",
        ErrorMessage = "Логин должен содержать только английские буквы и цифры, и не может быть пустым")]
    public string? NewLogin { get; init; }

    public string? NewPassword { get; init; }

    public List<int>? NewRoleIds { get; init; }
}