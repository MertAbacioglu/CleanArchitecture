using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Default Number Of Days")]
    [Range(1, 99, ErrorMessage = "{0} must be between {1} and {2}")]
    public int DefaultDays { get; set; }
}