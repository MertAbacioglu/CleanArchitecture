using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public ILeaveTypeService LeaveTypeService { get; set; }
    [Inject]
    public ILeaveAllocationService LeaveAllocationService { get; set; }
    [Inject]
    public IToastService ToastService { get; set; }
    public List<LeaveTypeVM> LeaveTypes { get; set; }
    public string Message { get; set; }=string.Empty;

    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }
    protected void EditLeaveType(int id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    protected void AllocateLeaveType(int id)
    {
        LeaveAllocationService.CreateLeaveAllocations(id);
    }
    protected void DetailsLeaveType(int id)
    {
          NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(int id)
    {
        Response<Guid> response = await LeaveTypeService.DeleteLeaveType(id);
        if (response.Success)
        {
            ToastService.ShowSuccess("Leave Type Deleted");
            //StateHasChanged();
            await OnInitializedAsync();
        }
        else
        {
            Message = response.Message;
        }
    } 
    
    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypes();
    }

}