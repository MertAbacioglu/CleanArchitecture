using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveMenagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveMenagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeDetailsQueryHandlerTest
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>> _mockAppLogger;

    public GetLeaveTypeDetailsQueryHandlerTest()
    {
        _mockRepo = MockLeaveRepository.GetLeaveTypeMockLeaveTypeRepository();
        _mockAppLogger = new Mock<IAppLogger<GetLeaveTypeDetailsQueryHandler>>();

        MapperConfiguration mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }


    [Fact]
    public async Task GetLeaveTypeDetailsTest()
    {
        GetLeaveTypeDetailsQueryHandler handler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);
        LeaveTypeDto result = await handler.Handle(new GetLeaveTypeDetailsQuery(Id:1), CancellationToken.None);
        result.ShouldBeOfType<LeaveTypeDto>();
        result.Name.ShouldBe("Test Vacation");
        Assert.Equal(10, result.DefaultDays);
    }
}
