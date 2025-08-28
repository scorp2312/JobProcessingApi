namespace InterviewGuide.DTOs;

using System.ComponentModel.DataAnnotations;

public class UpdateQuestionDto
{
   [Required]
   [MinLength(1)]
   [MaxLength(500)]
   public string? NewQuestion { get; set; }

   [Required]
   [MinLength(1)]
   [MaxLength(10000)]
   public string? NewAnswer { get; set; }

   public int? CategoryId { get; set; }
}