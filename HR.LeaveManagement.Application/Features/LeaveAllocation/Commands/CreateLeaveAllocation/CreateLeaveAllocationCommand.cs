using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public sealed record CreateLeaveAllocationCommand(int LeaveTypeId) : IRequest<Unit>;
