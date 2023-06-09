using HR.LeaveManagement.Domain.Entities;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveMenagement.Application.IntegrationTests
{
    public class HrDbContextTests
    {
        private HrDatabaseContext _hrDbContext;

        public HrDbContextTests()
        {
            DbContextOptions<HrDatabaseContext> dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _hrDbContext = new HrDatabaseContext(dbOptions);
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