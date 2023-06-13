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
    [MinLength(8, ErrorMessage = "{0} must be at least {1} characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "{0} must contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
    [Display(Name = "Password")]
    public string Password { get; set; }
}


