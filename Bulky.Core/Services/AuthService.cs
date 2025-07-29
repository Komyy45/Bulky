using Bulky.Core.Contracts.Ports.EmailService;
using Bulky.Core.Contracts.Ports.UrlService;
using Bulky.Core.Contracts.Services;
using Bulky.Core.Entities;
using Bulky.Core.Exceptions.Common;
using Bulky.Core.Exceptions.Identity;
using Bulky.Core.Models.Email;
using Bulky.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bulky.Core.Services
{
	public class AuthService(SignInManager<ApplicationUser> signInManager,
		UserManager<ApplicationUser> userManager, 
		IEmailService emailService,
		IUrlService urlService,
		ILogger<AuthService> logger) : IAuthService
	{
		// public object ConfirmAccount(object sendConfirmationDto)
		// {
		// 	throw new NotImplementedException();
		// }
		//
		// public object GetAccountDetails(string id)
		// {
		// 	throw new NotImplementedException();
		// }

		public async Task<SignInResult> Login(LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);

			if (user is null)
				throw new NotFoundException("User");
			
			var loginResult = await signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);

			return loginResult;
		}

		public async Task<bool> Register(RegisterDto registerDto)
		{
			var user = new ApplicationUser()
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				Name = registerDto.Name
			};
			
			var createUserResult = await userManager.CreateAsync(user, registerDto.Password);

			if (createUserResult.Errors.Any())
				foreach (var error in createUserResult.Errors)
					logger.LogError("Error", error.Description);
			
			
			var addRoleResult = await userManager.AddToRoleAsync(user, registerDto.Role);
	
			if(addRoleResult.Errors.Any())
				foreach (var error in createUserResult.Errors)
					logger.LogError("Error", error.Description);

			return createUserResult.Succeeded && addRoleResult.Succeeded;
		}

		public async Task Logout()
		{
			 await signInManager.SignOutAsync();
		}

		public async Task SendConfirmationEmail(string email)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user is null)
				throw new NotFoundException("User");

			var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

			var confirmationLink = urlService.BuildUrl("ConfirmAccount", "Account", "Identity", new { token, Id = user.Id });

			var message = new EmailMessage
			{
				To = email,
				From = "youssefelkomy500@gmail.com",
				Subject = "Confirm your email - Bulky",
				Body = $@"
		<html>
			<head>
				<style>
					body {{
						font-family: Arial, sans-serif;
						background-color: #f4f4f4;
						padding: 20px;
					}}
					.container {{
						background-color: #fff;
						padding: 30px;
						border-radius: 8px;
						box-shadow: 0 0 10px rgba(0,0,0,0.1);
					}}
					.button {{
						display: inline-block;
						margin-top: 20px;
						padding: 10px 20px;
						font-size: 16px;
						color: #fff;
						background-color: #007bff;
						border-radius: 5px;
						text-decoration: none;
					}}
				</style>
			</head>
			<body>
				<div class='container'>
					<h2>Welcome to YourApp, {user.UserName}!</h2>
					<p>Thank you for registering. Please confirm your email address by clicking the button below:</p>
					<a href='{confirmationLink}' class='button'>Confirm Email</a>
					<p>If the button doesn't work, copy and paste the following link into your browser:</p>
					<p><a href='{confirmationLink}'>{confirmationLink}</a></p>
					<br />
					<p>Best regards,<br/>The YourApp Team</p>
				</div>
			</body>
		</html>"
			};

			await emailService.SendEmail(message);
		}


		public async Task<bool> ConfirmAccount(string id, string token)
		{
			var user = await userManager.FindByIdAsync(id);

			if (user is null)
				throw new NotFoundException("User");

			var result = await userManager.ConfirmEmailAsync(user, token);

			if (result.Errors.Any())
				foreach (var error in result.Errors)
					logger.LogError("Error", error.Description);

			// Invalid or expired token
			return result.Succeeded;
		}


		// public object ResetPassword(object resetPasswordDto)
		// {
		// 	throw new NotImplementedException();
		// }
		//
		// public async Task<UpdatedAccountDto> UpdateAccount(UpdateAccountDto updateAccountDto)
		// {
		// 	var user = await userManager.FindByIdAsync(updateAccountDto.Id);
		//
		// 	user.Name = updateAccountDto.Name;
		// 	user.UserName = updateAccountDto.UserName;
		// 	user.Address = updateAccountDto.Address;
		// 	
		// 	var updateResult = await userManager.UpdateAsync(user);
		//
		// 	if (updateResult.Errors.Any())
		// 	{
		// 		foreach (var error in updateResult.Errors)
		// 			logger.LogError("Error ", error.Description);	
		// 	}
		// 	
		// 	return new UpdatedAccountDto(user.UserName, user.Email, user.Name, user.Address);
		// }
	}
}
