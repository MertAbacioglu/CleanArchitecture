using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain.Enums;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId { get => _httpContextAccessor.HttpContext?.User.FindFirstValue("uid"); }
    public string UserEmail { get => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email); }

    public async Task<Employee> GetEmployee(string id)
    {
        ApplicationUser? employee = await _userManager.FindByIdAsync(id);
        if (employee == null)
        {
            throw new NotFoundException($"Employee with id {id} not found.", id);
        }
        return new Employee
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email
        };

    }

    public async Task<List<Employee>> GetEmployees()
    {
        IList<ApplicationUser> employees = await _userManager.GetUsersInRoleAsync(Role.Employee.ToString());

        return employees.Select(employee => new Employee
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email
        }).ToList();
    }
}
