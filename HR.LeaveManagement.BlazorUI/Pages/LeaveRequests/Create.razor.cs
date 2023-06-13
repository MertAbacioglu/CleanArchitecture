using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject]
        ILeaveTypeService LeaveTypeService { get; set; }
        [Inject]
        ILeaveRequestService LeaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        LeaveRequestVM LeaveRequest { get; set; } = new();
        List<LeaveTypeVM> leaveTypeVMs { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            leaveTypeVMs = await LeaveTypeService.GetLeaveTypes();
        }

        private async Task HandleValidSubmit()
        {
            await LeaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}