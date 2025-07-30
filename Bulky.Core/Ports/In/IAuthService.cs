using Bulky.Core.Application.Models.Common;
using Bulky.Core.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Bulky.Core.Ports.In
{
	public interface IAuthService
	{
		public Task<Result<SignInResult>> Login(LoginDto loginDto);
		public Task<Result<IdentityResult>> Register(RegisterDto registerDto);
		public Task Logout();
		public Task<Result> SendConfirmationEmail(string email);
		public Task<Result<IdentityResult>> ConfirmAccount(string id, string token);

		// public object UpdateAccount(object updateAccountDto);
		//
		// public object GetAccountDetails(string id);
		//
		// public object ResetPassword(object resetPasswordDto);
	}
}
