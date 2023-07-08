using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Command.DeleteLeaveType;

public sealed record DeleteLeaveTypeCommand(int Id) : IRequest<Unit>;
