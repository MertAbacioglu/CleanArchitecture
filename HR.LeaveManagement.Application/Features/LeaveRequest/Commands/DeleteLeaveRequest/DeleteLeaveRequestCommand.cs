using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public sealed record DeleteLeaveRequestCommand(int Id) : IRequest;
