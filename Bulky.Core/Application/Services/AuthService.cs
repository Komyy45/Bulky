using Bulky.Core.Application.Models.Common;
using Bulky.Core.Application.Models.Email;
using Bulky.Core.Application.Models.Identity;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Ports.In;
using Bulky.Core.Ports.Out;
using Microsoft.AspNetCore.Identity;
namespace Bulky.Core.Application.Services
{
	public class AuthService(SignInManager<ApplicationUser> signInManager,
		UserManager<ApplicationUser> userManager,
		IEmailService emailService,
		IUrlService urlService) : IAuthService
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

		public async Task<Result<SignInResult>> Login(LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);

			if (user is null)
				return Result<SignInResult>.Failure(Error.NotFound("User doesn't exist, try to Regsiter first"));

			var loginResult = await signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);

			return Result<SignInResult>.Success(loginResult);
		}

		public async Task<Result<IdentityResult>> Register(RegisterDto registerDto)
		{
			var user = new ApplicationUser()
			{
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				Name = registerDto.Name
			};

			var createUserResult = await userManager.CreateAsync(user, registerDto.Password);

			if (createUserResult.Errors.Any())
			{
				var errors = createUserResult.Errors.Select(e => Error.ValidationError(e.Description)).ToArray();
				return Result<IdentityResult>.Failure(errors);
			}

			var addRoleResult = await userManager.AddToRoleAsync(user, registerDto.Role);

			if (addRoleResult.Errors.Any())
			{
				var errors = addRoleResult.Errors.Select(e => Error.ValidationError(e.Description)).ToArray();
				return Result<IdentityResult>.Failure(errors);
			}


			return Result<IdentityResult>.Success(createUserResult);
		}


		public async Task Logout()
		{
			await signInManager.SignOutAsync();
		}

		public async Task<Result> SendConfirmationEmail(string email)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user is null)
				return Result.Failure(Error.NotFound("User doesn't exists"));

			var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

			var confirmationLink = urlService.BuildUrl("ConfirmAccount", "Account", "Identity", new { token, user.Id });

			var message = new EmailMessage
			{
				To = email,
				From = "youssefelkomy500@gmail.com",
				Subject = "Confirm your email - Bulky",
				Body = BuildConfirmationEmailMessage(user.UserName!, "https://localhost:7032/" + confirmationLink)
			};

			await emailService.SendEmail(message);

			return Result.Success();
		}



		public async Task<Result<IdentityResult>> ConfirmAccount(string id, string token)
		{
			var user = await userManager.FindByIdAsync(id);

			if (user is null)
				return Result<IdentityResult>.Failure(Error.NotFound("User not Found"));

			var result = await userManager.ConfirmEmailAsync(user, token);

			if (result.Errors.Any())
			{
				var errors = result.Errors.Select(e => Error.ValidationError(e.Description)).ToArray();
				return Result<IdentityResult>.Failure(errors);
			}

			return Result<IdentityResult>.Success(result);
		}

		private string BuildConfirmationEmailMessage(string username, string confirmationLink)
		{
			return $@"
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
					<h2>Welcome to YourApp, {username}!</h2>
					<p>Thank you for registering. Please confirm your email address by clicking the button below:</p>
					<a href='{confirmationLink}' class='button'>Confirm Email</a>
					<p>If the button doesn't work, copy and paste the following link into your browser:</p>
					<p><a href='{confirmationLink}'>{confirmationLink}</a></p>
					<br />
					<p>Best regards,<br/>The YourApp Team</p>
				</div>
			</body>
		</html>";
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
