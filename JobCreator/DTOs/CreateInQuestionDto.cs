namespace JobCreator.DTOs;

using JobCreator.Models;

public class CreateInQuestionDto
{
    public required string Question { get; set; }

    public string? Answer { get; set; }

    public required int CategoryId { get; set; }
}