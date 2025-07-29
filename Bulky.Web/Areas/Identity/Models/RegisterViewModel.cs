using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Areas.Identity.Models;

public class RegisterViewModel
{
    [Required]
    public string Username { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, Compare("Password")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
	public string Role { get; set; }
}