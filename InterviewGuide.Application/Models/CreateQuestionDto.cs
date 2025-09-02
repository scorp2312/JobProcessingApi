namespace InterviewGuide.Application.Models;

using System.ComponentModel.DataAnnotations;

public class CreateQuestionDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public required string QuestionText { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(10000)]
    public required string Answer { get; set; }

    public required int CategoryId { get; set; }
}