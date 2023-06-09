using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set;}
}
