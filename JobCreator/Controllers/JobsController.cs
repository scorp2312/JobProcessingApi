namespace JobCreator.Controllers;

using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class JobsController(IJobService jobService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobDto createJobDto)
    {
        var job = await jobService.CreateJobAsync(createJobDto);
        return this.CreatedAtAction(nameof(this.GetJob), new { id = job.Id }, job);
    }

    [HttpGet]
    public async Task<ActionResult<List<JobDto>>> GetAllJobs()
    {
        var jobs = await jobService.GetAllJobsAsync();
        return this.Ok(jobs);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobDto>> GetJob(Guid id)
    {
        var job = await jobService.GetJobByIdAsync(id);
        if (job == null)
        {
            return this.NotFound();
        }

        return this.Ok(job);
    }

    [HttpGet("search")]
    public async Task<ActionResult<JobDto>> GetJobBySearch(
        [FromQuery] string q,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var jobs = await jobService.GetJobBySearchAsync(q, pageIndex, pageSize);
        return this.Ok(jobs);
    }
}