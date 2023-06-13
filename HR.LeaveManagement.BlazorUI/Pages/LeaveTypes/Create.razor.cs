using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Create
{
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public LeaveTypeVM LeaveType { get; set; } = new();

    [Inject]
    public IToastService ToastService { get; set; }
    private async Task HandleValidSubmit()
    {

        Response<Guid> response = await LeaveTypeService.CreateLeaveType(LeaveType);
        if (response.Success)
        {
            ToastService.ShowSuccess("Leave Type Created");
            NavigationManager.NavigateTo("/leavetypes/");
        }
    }
}