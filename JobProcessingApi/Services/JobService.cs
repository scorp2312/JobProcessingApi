using JobProcessingApi.Data;
using JobProcessingApi.DTOs;
using JobProcessingApi.Messages;
using JobProcessingApi.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace JobProcessingApi.Services;

public interface IJobService
{
    Task<JobDto> CreateJobAsync(CreateJobDto createJobDto);
    Task<List<JobDto>> GetAllJobsAsync();
    Task<JobDto?> GetJobByIdAsync(Guid id);
}

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public JobService(
        ApplicationDbContext context, 
        IPublishEndpoint publishEndpoint)
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
            Status = JobStatus.Created
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        
        await _publishEndpoint.Publish(new JobCreatedEvent
        {
            JobId = job.Id,
            CreatedAt = job.CreatedAt
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

    private static JobDto MapToDto(Job job)
    {
        return new JobDto
        {
            Id = job.Id,
            CreatedAt = job.CreatedAt,
            CompletedAt = job.CompletedAt,
            Status = job.Status.ToString()
        };
    }
}