﻿using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Contracts.Base;

public interface ILeaveTypeService
{
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    Task<List<LeaveTypeVM>> GetLeaveTypes();
    Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM);
    Task<Response<Guid>> UpdateLeaveType(int id,LeaveTypeVM leaveTypeVM);
    Task<Response<Guid>> DeleteLeaveType(int id);


}
