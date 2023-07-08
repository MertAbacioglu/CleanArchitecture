using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts.Base;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;
    public LeaveTypeService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeVM leaveTypeVM)
    {
        try
        {
            CreateLeaveTypeCommand leaveTypeDto = _mapper.Map<CreateLeaveTypeCommand>(leaveTypeVM);
            await _client.LeaveTypesPOSTAsync(leaveTypeDto);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {

            return ConvertApiExceptions<Guid>(ex);
        }

    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {

            return ConvertApiExceptions<Guid>(ex);
        }


    }

    public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
    {
        LeaveTypeDetailsDto leaveType = await _client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);

    }

    public async Task<List<LeaveTypeVM>> GetLeaveTypes()
    {
        ICollection<LeaveTypeDto> leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
    }

    public async Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM leaveTypeVM)
    {
        try
        {
            UpdateLeaveTypeCommand updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveTypeVM);
            await _client.LeaveTypesPUTAsync(updateLeaveTypeCommand);
            return new Response<Guid>() { Success = true };
        }
        catch (ApiException ex)
        {

            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
