using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public sealed record CreateLeaveRequestCommand(string RequestComments) : BaseLeaveRequest, IRequest<Unit>;
