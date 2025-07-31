using JobCreator.DTOs;
using JobCreator.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobCreator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobDto createJobDto)
    {
        var job = await _jobService.CreateJobAsync(createJobDto);
        return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
    }

    [HttpGet]
    public async Task<ActionResult<List<JobDto>>> GetAllJobs()
    {
        var jobs = await _jobService.GetAllJobsAsync();
        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobDto>> GetJob(Guid id)
    {
        var job = await _jobService.GetJobByIdAsync(id);
        if (job == null)
        {
            return NotFound();
        }
        return Ok(job);
    }
}