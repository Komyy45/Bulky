using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Areas.Identity.Models;

public class RegisterViewModel : AuthViewModel
{
    [Required]
    public string Username { get; set; }

	[Required, Compare("Password")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
	public string Role { get; set; }
}