namespace JobCreator.Services;

using JobCreator.DTOs;

public interface IJobService
{
    Task<JobDto> CreateJobAsync(CreateJobDto createJobDto);

    Task<List<JobDto>> GetAllJobsAsync();

    Task<JobDto?> GetJobByIdAsync(Guid id);

    Task MarkJobAsInProgressAsync(Guid id);

    Task MarkJobAsCompletedAsync(Guid jobId, DateTime completedAt, string? result);
}