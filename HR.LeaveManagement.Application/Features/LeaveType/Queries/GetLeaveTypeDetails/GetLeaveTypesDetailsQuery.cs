using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;


public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;