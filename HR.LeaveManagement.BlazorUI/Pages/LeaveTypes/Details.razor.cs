using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Details
{
    [Inject]
    ILeaveTypeService LeaveTypeService { get; set; }

    [Parameter]
    public int id { get; set; }

    LeaveTypeVM LeaveType = new LeaveTypeVM();

    protected async override Task OnParametersSetAsync()
    {
        LeaveType = await LeaveTypeService.GetLeaveTypeDetails(id);
    }
}