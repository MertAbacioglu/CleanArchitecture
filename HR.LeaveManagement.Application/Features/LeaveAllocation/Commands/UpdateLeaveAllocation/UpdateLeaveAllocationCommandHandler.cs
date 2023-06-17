using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

    }
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {

        // convert to domain entity object
        Domain.Entities.LeaveAllocation? leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        // check if leave allocation is null
        if (leaveAllocation is null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        ArgumentNullException.ThrowIfNull(request.LeaveTypeId);

        // map request to leave allocation
        _mapper.Map(request, leaveAllocation); //check this


        // add to database
        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }

}
