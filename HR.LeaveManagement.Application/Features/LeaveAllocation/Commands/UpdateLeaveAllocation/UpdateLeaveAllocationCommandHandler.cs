using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

    }
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        UpdateLeaveAllocationCommandValidator validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            //todo : use logger
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        }

        // convert to domain entity object
        Domain.Entities.LeaveAllocation? leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);


        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        // map request to leave allocation
        _mapper.Map(request, leaveAllocation); //check this


        // add to database
        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);


        // return Unit value
        return Unit.Value;
    }

}
