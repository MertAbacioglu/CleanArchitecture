namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public abstract record BaseLeaveRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
    }
}
