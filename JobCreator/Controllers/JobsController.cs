namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService jobService;

    public JobsController(IJobService jobService)
    {
        this.jobService = jobService;
    }

    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobDto createJobDto)
    {
        var job = await this.jobService.CreateJobAsync(createJobDto);
        return this.CreatedAtAction(nameof(this.GetJob), new { id = job.Id }, job);
    }

    [HttpGet]
    public async Task<ActionResult<List<JobDto>>> GetAllJobs()
    {
        var jobs = await this.jobService.GetAllJobsAsync();
        return this.Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobDto>> GetJob(Guid id)
    {
        var job = await this.jobService.GetJobByIdAsync(id);
        if (job == null)
        {
            return this.NotFound();
        }

        return this.Ok(job);
    }
}