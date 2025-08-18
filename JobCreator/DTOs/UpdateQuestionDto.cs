namespace JobCreator.DTOs;

using JobCreator.Models;

public class UpdateQuestionDto
{
   public Guid Id { get; set; }

   public string? Newquestion { get; set; }

   public string? NewAnswer { get; set; }

   public Category? NewCategory { get; set; }
}