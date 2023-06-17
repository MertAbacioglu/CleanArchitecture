using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorUI.Models;

public class LoginVM
{
    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "Invalid {0} format")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}


