using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;

public sealed record DeleteLeaveAllocationCommand(int Id) : IRequest;