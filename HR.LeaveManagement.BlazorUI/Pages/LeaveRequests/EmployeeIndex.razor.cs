using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex
{
    [Inject] ILeaveRequestService leaveRequestService { get; set; }
    [Inject] IJSRuntime js { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    public EmployeeLeaveRequestViewVM Model { get; set; } = new();
    public string Message { get; set; } = string.Empty;

    protected async override Task OnInitializedAsync()
    {

        Model = await leaveRequestService.GetUserLeaveRequests();
    }

    async Task CancelRequestAsync(int id)
    {
        bool confirm = await js.InvokeAsync<bool>("confirm", "Do you want to cancel this request?");
        if (confirm)
        {
            Services.Base.Response<Guid> response = await leaveRequestService.CancelLeaveRequest(id);
            if (response.Success)
            {
                StateHasChanged();
            }
            else
            {
                Message = response.Message;
            }
        }

    }
}