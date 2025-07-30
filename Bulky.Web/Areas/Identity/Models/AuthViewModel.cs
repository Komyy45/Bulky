using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Areas.Identity.Models
{
	public class AuthViewModel
	{
		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
