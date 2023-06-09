using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository) 
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        RuleFor(p => p.NumberOfDays).GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}");
        RuleFor(p => p.Period).GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue");
        RuleFor(p => p.LeaveTypeId).GreaterThan(0).MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist.");
        RuleFor(p => p.Id).MustAsync(LeaveAllocationNameUnique).WithMessage("{PropertyName} must be present");
        
    }

    private async Task<bool> LeaveAllocationNameUnique(int id, CancellationToken arg2)
    {
        Domain.Entities.LeaveAllocation leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken arg2)
    {
        Domain.Entities.LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}


