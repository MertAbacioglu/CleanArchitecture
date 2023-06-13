using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //validate incoming data
        //UpdateLeaveTypeCommandValidator validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        //ValidationResult validationResult = await validator.ValidateAsync(request);

        //if (validationResult.Errors.Any())
        //{
        //    _logger.LogInformation($"Validation errors in update request for {nameof(LeaveType)} - {request.Id}");
        //    throw new FluentValidation.ValidationException("Invalid Leave type");
        //}

        Domain.Entities.LeaveType leaveTypeToUpdate = _mapper.Map<Domain.Entities.LeaveType>(request);

        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}
