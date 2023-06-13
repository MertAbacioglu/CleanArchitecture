using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain.Entities;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace HR.LeaveMenagement.Application.IntegrationTests
{
    public class HrDbContextTests
    {
        private HrDatabaseContext _hrDbContext;
        private readonly string _userId;
        private readonly Mock<IUserService> _userServiceMock;
        public HrDbContextTests()
        {
            DbContextOptions<HrDatabaseContext> dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _userId = "00000000-0000-0000-0000-000000000000";
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(m => m.UserId).Returns(_userId);
            _hrDbContext = new HrDatabaseContext(dbOptions, _userServiceMock.Object);
        }

        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            //Arrange
            LeaveType leaveType = new()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            //Act
            await _hrDbContext.LeaveTypes.AddAsync(leaveType);
            await _hrDbContext.SaveChangesAsync();

            //Assert
            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async void Save_SetDateModifiedValue()
        {
            //Arrange
            LeaveType leaveType = new()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            //Act
            await _hrDbContext.LeaveTypes.AddAsync(leaveType);
            await _hrDbContext.SaveChangesAsync();

            //Assert
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}