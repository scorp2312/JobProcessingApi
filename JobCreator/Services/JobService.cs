namespace JobCreator.Services;

using JobCreator.Data;
using JobCreator.DTOs;
using JobCreator.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Messages.Events;

public class JobService(
    ApplicationDbContext context,
    IPublishEndpoint publishEndpoint)
{
    public async Task<JobDto> CreateJobAsync(CreateJobDto createJobDto)
    {
        var job = new Job
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Status = JobStatusEnum.Created,
            Description = createJobDto.Description,
        };

        context.Jobs.Add(job);
        await context.SaveChangesAsync();

        await publishEndpoint.Publish(new JobCreatedEvent
        {
            JobId = job.Id,
        });

        return MapToDto(job);
    }

    public async Task<List<JobDto>> GetAllJobsAsync()
    {
        var jobs = await context.Jobs
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();

        return jobs.Select(MapToDto).ToList();
    }

    public async Task<JobDto?> GetJobByIdAsync(Guid id)
    {
        var job = await context.Jobs.FindAsync(id);
        return job != null ? MapToDto(job) : null;
    }

    public async Task<PaginatedList<JobDto>> GetJobBySearchAsync(string q, int pageIndex = 1, int pageSize = 20)
    {
        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var query = context.Jobs.AsQueryable();
        if (!string.IsNullOrEmpty(q))
        {
            var lowerQ = q.ToLower();
            query = query.Where(job =>
                job.Description != null && job.Description.ToLower().Contains(lowerQ));
        }

        int totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(j => j.CreatedAt)
            .ThenBy(j => j.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(job => new JobDto
            {
                Id = job.Id,
                CreatedAt = job.CreatedAt,
                Description = job.Description,
                Status = job.Status.ToString(),
                CompletedAt = job.CompletedAt,
                Result = job.Result,
            })
            .AsNoTracking()
            .ToListAsync();

        return new PaginatedList<JobDto>(items, totalItems, pageIndex, pageSize);
    }

    public async Task MarkJobAsInProgressAsync(Guid id)
    {
        var job = await context.Jobs.FindAsync(id);
        if (job != null)
        {
            job.Status = JobStatusEnum.InProgress;
            await context.SaveChangesAsync();
        }
    }

    public async Task MarkJobAsCompletedAsync(Guid jobId, DateTime completedAt, string? result)
    {
        var job = await context.Jobs.FindAsync(jobId);
        if (job != null)
        {
            job.Status = JobStatusEnum.Completed;
            job.CompletedAt = completedAt;
            job.Result = result;
            await context.SaveChangesAsync();
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
            Result = job.Result,
        };
    }
}