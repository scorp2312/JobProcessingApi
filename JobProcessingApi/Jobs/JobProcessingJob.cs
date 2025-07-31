using JobProcessingApi.Services;
using Quartz;

namespace JobProcessingApi.Jobs;

public class JobProcessingJob : IJob
{
    private readonly IJobProcessingService _jobProcessingService;

    public JobProcessingJob(IJobProcessingService jobProcessingService)
    {
        _jobProcessingService = jobProcessingService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _jobProcessingService.ProcessPendingJobsAsync();
        }
        catch (Exception ex)
        {
            throw new JobExecutionException(ex);
        }
    }
}