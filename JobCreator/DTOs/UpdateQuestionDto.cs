namespace JobCreator.DTOs;

using System.ComponentModel.DataAnnotations;
using JobCreator.Models;

public class UpdateQuestionDto
{
   public Guid Id { get; set; }

   [Required]
   [MinLength(1)]
   [MaxLength(500)]
   public required string NewQuestion { get; set; }

   [Required]
   [MinLength(1)]
   [MaxLength(10000)]
   public required string NewAnswer { get; set; }

   public required int CategoryId { get; set; }
}