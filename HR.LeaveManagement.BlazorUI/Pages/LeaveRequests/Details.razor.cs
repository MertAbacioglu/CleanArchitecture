using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Services;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class Details
{
    [Inject] ILeaveRequestService LeaveRequestService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Parameter] public int id { get; set; }

    string ClassName;
    string HeadingText;

    public LeaveRequestVM Model { get; private set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await LeaveRequestService.GetLeaveRequest(id);
    }

    protected override async Task OnInitializedAsync()
    {
        if (Model.Approved == null)
        {
            ClassName = "warning";
            HeadingText = "Pending Approval";
        }
        else if (Model.Approved == true)
        {
            ClassName = "success";
            HeadingText = "Approved";
        }
        else
        {
            ClassName = "danger";
            HeadingText = "Rejected";
        }
    }

    async Task ChangeApproval(bool approvalStatus)
    {
        await LeaveRequestService.ApproveLeaveRequest(id, approvalStatus);
        NavigationManager.NavigateTo("/leaverequests/");
    }
}