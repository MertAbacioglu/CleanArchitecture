using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public sealed record UpdateLeaveTypeCommand(int Id, string Name, int DefaultDays) : IRequest<Unit>;
}