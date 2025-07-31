using JobProcessingApi.Data;
using JobProcessingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobProcessingApi.Services;

public interface IJobProcessingService
{
    Task ProcessPendingJobsAsync();
}

public class JobProcessingService : IJobProcessingService
{
    private readonly IServiceProvider _serviceProvider;

    public JobProcessingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ProcessPendingJobsAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingJobs = await context.Jobs
            .Where(j => j.Status == JobStatus.Created)
            .ToListAsync();

        if (pendingJobs.Any())
        {
            foreach (var job in pendingJobs)
            {
                await ProcessJobAsync(context, job);
            }
        }
    }

    private async Task ProcessJobAsync(ApplicationDbContext context, Job job)
    {
        
           
            job.Status = JobStatus.InProgress;
            await context.SaveChangesAsync();
            
            await Task.Delay(5000);
           
            job.Status = JobStatus.Completed;
            job.CompletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        
    }
}