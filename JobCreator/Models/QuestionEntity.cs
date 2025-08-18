namespace JobCreator.Models;

using System.ComponentModel.DataAnnotations;

public class QuestionEntity
{
    public required Guid Id { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(500)]
    public required string QuestionText { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(10000)]
    public required string Answer { get; set; }

    public required CategoryEntity CategoryEntity { get; set; }
}