using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveMenagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveMenagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTest
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

    public GetLeaveTypeListQueryHandlerTest()
    {
        _mockRepo = MockLeaveRepository.GetLeaveTypeMockLeaveTypeRepository();

        MapperConfiguration mapperConfig = new MapperConfiguration(c => {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        GetLeaveTypesQueryHandler handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
        //List<LeaveTypeDto> result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);
        List<LeaveTypeDto> result = null;
       result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
        result[0].Name.ShouldBe("Test Vacation");
    }
}
