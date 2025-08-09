using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;

namespace JobCreator.Services;

public interface IJobService
{
    Task<JobDto> CreateJobAsync(CreateJobDto createJobDto);
    Task<List<JobDto>> GetAllJobsAsync();
    Task<JobDto?> GetJobByIdAsync(Guid id);
    Task MarkJobAsInProgressAsync(Guid id);
    Task MarkJobAsCompletedAsync(Guid jobId, DateTime completedAt, string? result);
}

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public JobService(
        ApplicationDbContext context, 
        IPublishEndpoint publishEndpoint
        )
    {
        _context = context;
        _publishEndpoint = publishEndpoint;

    }

    public async Task<JobDto> CreateJobAsync(CreateJobDto createJobDto)
    {
        var job = new Job
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Status = JobStatus.Created,
            Description = createJobDto.Description
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        
        
        await _publishEndpoint.Publish(new JobCreatedEvent
        {
            JobId = job.Id,
            CreatedAt = job.CreatedAt,
            Description = job.Description
        });
        

        return MapToDto(job);
    }

    public async Task<List<JobDto>> GetAllJobsAsync()
    {
        var jobs = await _context.Jobs
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();

        return jobs.Select(MapToDto).ToList();
    }

    public async Task<JobDto?> GetJobByIdAsync(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);
        return job != null ? MapToDto(job) : null;
    }

    public async Task MarkJobAsInProgressAsync(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job != null)
        {
            job.Status = JobStatus.InProgress;
            await _context.SaveChangesAsync();
        }
    }
    public async Task MarkJobAsCompletedAsync(Guid jobId, DateTime completedAt, string? result)
    {
        var job = await _context.Jobs.FindAsync(jobId);
        if (job != null)
        {
            job.Status = JobStatus.Completed;
            job.CompletedAt = completedAt;
            job.Result = result;
            await _context.SaveChangesAsync();
        }
        
    }

    private static JobDto MapToDto(Job job)
    {
        return new JobDto
        {
            Id = job.Id,
            CreatedAt = job.CreatedAt,
            CompletedAt = job.CompletedAt,
            Status = job.Status.ToString(),
            Description = job.Description,
            Result = job.Result
        };
    }
}