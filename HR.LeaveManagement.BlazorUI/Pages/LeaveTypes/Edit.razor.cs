using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Edit
{
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    public LeaveTypeVM LeaveType { get; set; } = new();

    [Parameter]
    public int Id { get; set; }
    public string Message { get; private set; }

    protected override async Task OnParametersSetAsync()
    {
        LeaveType = await LeaveTypeService.GetLeaveTypeDetails(Id);
    }

    private async Task EditLeaveType()
    {
        Response<Guid> response = await LeaveTypeService.UpdateLeaveType(Id, LeaveType);
        if (response.Success)
        {
            NavigationManager.NavigateTo("/leavetypes/");
        }
        Message = response.Message;
    }
}