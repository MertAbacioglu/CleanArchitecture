using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public sealed record ChangeLeaveRequestApprovalCommand(
    int Id,
    bool Approved
) : IRequest<Unit>;
