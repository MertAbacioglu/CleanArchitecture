using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;


namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{

    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;


    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }



    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.LeaveRequest> leaveRequests = new();
        List<LeaveRequestListDto> requests = new();

        //check if user is logged in
        if (request.IsLoggedInUser)
        {
            string userId = _userService.UserId;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);
            Models.Identity.Employee employee = await _userService.GetEmployee(userId);
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (LeaveRequestListDto requestDto in requests)
                requestDto.Employee = employee;
            
            return requests;
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (LeaveRequestListDto requestDto in requests)requestDto.Employee = await _userService.GetEmployee(requestDto.RequestingEmployeeId);           
        }

        return requests;
    }
}