namespace InterviewGuide.Controllers;

using InterviewGuide.Application.Models;
using InterviewGuide.Application.Services;
using InterviewGuide.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Users")]

public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet("Find")]
    public async Task<PaginatedList<UserDto>> FindUsersAsync(
        [FromQuery] string? login,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var users = await userService.FindAsync(login, pageIndex, pageSize);

        return users;
    }

    [HttpPost]
    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        return await userService.CreateUserAsync(userDto);
    }

    [HttpGet]
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        return await userService.GetAllUsersAsync();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, UpdateUserDto data)
    {
        var changed = await userService.UpdateUserAsync(id, data);
        return changed ? this.Ok() : this.NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var deleted = await userService.DeleteUserAsync(id);
        return deleted ? this.Ok() : this.NotFound();
    }
}