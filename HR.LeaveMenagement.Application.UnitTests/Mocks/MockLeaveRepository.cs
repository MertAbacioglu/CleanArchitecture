using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveMenagement.Application.UnitTests.Mocks
{
    public class MockLeaveRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeMockLeaveTypeRepository()
        {
            List<LeaveType> leaveTypes = new List<LeaveType>
            {
                new LeaveType {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Test Vacation"
                },
                new LeaveType {
                    Id = 2,
                    DefaultDays = 15,
                    Name = "Test Sick"
                },
                new LeaveType {
                    Id = 3,
                    DefaultDays = 10,
                    Name = "Test Maternity"
                },
            };

            Mock<ILeaveTypeRepository> mockRepo = new Mock<ILeaveTypeRepository>();//creates a mock of ILeaveTypeRepository
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);//returns a list of leaveTypes when GetAsync() is called
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => leaveTypes.FirstOrDefault(l => l.Id == id));//returns a leaveType when Get() is called
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });//adds a leaveType to the list when CreateAsync() is called and returns a completed task
            return mockRepo;
        }
    }
}