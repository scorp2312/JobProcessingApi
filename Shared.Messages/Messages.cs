namespace Shared.Messages;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileNameMustMatchTypeName", Justification = "Reviewed.")]
public class JobCreatedEvent
{
    public Guid JobId { get; set; }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]

public class JobCompletedEvent
{
    public Guid JobId { get; set; }

    public DateTime CompletedAt { get; set; }

    public string? Result { get; set; }
}

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]

public class JobInProgressEvent
{
    /// <summary>
    /// Gets or Sets.
    /// </summary>
    public Guid JobId { get; set; }
}