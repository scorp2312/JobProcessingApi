namespace JobCreator.Models;

using System.ComponentModel.DataAnnotations;

public class QuestionEntity
{
    public required Guid Id { get; set; }

    public required string QuestionText { get; set; }

    public required string Answer { get; set; }

    public required CategoryEntity CategoryEntity { get; set; }
}