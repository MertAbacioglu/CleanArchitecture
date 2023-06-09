using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity;

public interface IUserService
{
    Task<Employee> GetEmployee(string id);
    Task<List<Employee>> GetEmployees();
    public string UserId { get; }
}
