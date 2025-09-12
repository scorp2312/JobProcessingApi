namespace InterviewGuide.Application.Services;

using InterviewGuide.Application.Models;
using InterviewGuide.Domain.Entities;
using InterviewGuide.Domain.Exceptions;
using InterviewGuide.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

public class UserService(IUserRepository userRepository, IRepository<RoleEntity, int> roleRepository)
{
    public async Task<PaginatedList<UserDto>> FindAsync(string? login, int pageIndex, int pageSize)
    {
        var paginatedUsers = await userRepository.FindAsync(login, pageIndex, pageSize);

        var usersDtos = paginatedUsers.Items.ConvertAll(MapUserToDto);

        return new PaginatedList<UserDto>(usersDtos, paginatedUsers.TotalItems, paginatedUsers.PageIndex, paginatedUsers.TotalPages);
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var users = await userRepository.GetAllAsync();
        if (users.Any(u => u.Login == userDto.Login))
        {
            throw new BusinessException("пользователь с таким именем уже существует", StatusCodes.Status400BadRequest);
        }

        var roles = await roleRepository.GetAllAsync();
        var userRoles = roles.Where(role => userDto.RoleIds.Contains(role.Id)).ToList();

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        var user = new UserEntity
        {
            Login = userDto.Login,
            PasswordHash = passwordHash,
            Roles = userRoles,
        };
        await userRepository.AddAsync(user);
        return MapUserToDto(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.OrderBy(c => c.Id).Select(MapUserToDto).ToList();
    }

    public async Task<bool> UpdateUserAsync(Guid id, UpdateUserDto userDto)
    {
        var user = await userRepository.GetAsync(id)
            ?? throw new NotFoundException(id.ToString());

        if (userDto.NewLogin != null)
        {
            var users = await userRepository.GetAllAsync();

            if (users.Any(u => u.Login == userDto.NewLogin))
            {
                throw new BusinessException("пользователь с таким именем уже существует", StatusCodes.Status400BadRequest);
            }

            user.Login = userDto.NewLogin;
        }

        if (userDto.NewPassword != null)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.NewPassword);
        }

        if (userDto.NewRoleIds != null)
        {
            var roles = await roleRepository.GetAllAsync();
            var newUserRoles = roles.Where(role => userDto.NewRoleIds.Contains(role.Id)).ToList();
            user.Roles = newUserRoles;
        }

        return await userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await userRepository.GetAsync(id)
            ?? throw new NotFoundException(id.ToString());
        return await userRepository.DeleteAsync(user);
    }

    private static UserDto MapUserToDto(UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            RoleIds = user.Roles.Select(r => r.Id).ToList(),
        };
    }
}