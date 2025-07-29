using Bulky.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bulky.Core.Contracts.Services
{
	public interface IAuthService
	{
		public Task<SignInResult> Login(LoginDto loginDto);
		public Task<bool> Register(RegisterDto registerDto);
		public Task Logout();
		public Task SendConfirmationEmail(string email);
		public Task<bool> ConfirmAccount(string id, string token);

		// public object UpdateAccount(object updateAccountDto);
		//
		// public object GetAccountDetails(string id);
		//
		// public object ResetPassword(object resetPasswordDto);
	}
}
