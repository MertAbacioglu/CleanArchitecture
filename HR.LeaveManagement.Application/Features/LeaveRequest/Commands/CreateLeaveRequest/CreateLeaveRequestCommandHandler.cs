using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IUserService _userService;

    public CreateLeaveRequestCommandHandler(IEmailSender emailSender,
        IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, ILeaveAllocationRepository leaveAllocationRepository, IUserService userService)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        CreateLeaveRequestCommandValidator validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        // Get requesting employee's id
        string employeeId = _userService.UserId;

        // Check on employee's allocation
        Domain.Entities.LeaveAllocation allocation =await _leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);

        // if allocations aren't enough, return validation error with message
        if (allocation is null)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId), $"You don't have any allocations for this leave type"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
        }

        int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
        if (daysRequested > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.EndDate), "You do not have enough days for this request"));
            throw new BadRequestException("Invalid Leave Request", validationResult);
            //todo : make this exceptions custom by the exception type
        }

        // Create leave request
        Domain.Entities.LeaveRequest leaveRequest = _mapper.Map<Domain.Entities.LeaveRequest>(request);
        leaveRequest.DateRequested = DateTime.Now;
        await _leaveRequestRepository.CreateAsync(leaveRequest);

        // send confirmation email

        try
        {
            EmailMessage email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

        await _emailSender.SendEmail(email);

        }
        catch (Exception ex)
        {
            //log or handle error
            Console.WriteLine(ex.Message);
            
        }

       

        return Unit.Value;
    }
}
