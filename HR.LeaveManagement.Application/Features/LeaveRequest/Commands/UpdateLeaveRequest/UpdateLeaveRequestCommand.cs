using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public sealed record UpdateLeaveRequestCommand(
    int Id,
    string RequestComments,
    bool Cancelled,
    bool Approved
) : BaseLeaveRequest, IRequest<Unit>;