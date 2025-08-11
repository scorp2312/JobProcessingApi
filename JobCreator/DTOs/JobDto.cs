namespace JobCreator.DTOs;

using System.Diagnostics.CodeAnalysis;

public class JobDto
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Result { get; set; }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
public class CreateJobDto
{
    public string? Description { get; set; }
}