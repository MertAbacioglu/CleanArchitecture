using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public sealed record CancelLeaveRequestCommand(int Id) : IRequest<Unit>;