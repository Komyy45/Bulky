using Bulky.Core.Contracts.Services;
using Bulky.Core.Exceptions.Identity;
using Bulky.Core.Models.Identity;
using Bulky.Web.Areas.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Areas.Identity.Controllers
{
	public class AccountController(IAuthService authService) : Controller
	{
		public IActionResult Login()
		{
			
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{

			var registerDto = new RegisterDto(registerViewModel.Email, registerViewModel.Username, registerViewModel.Password, registerViewModel.Name, registerViewModel.Role);

			try
			{
				await authService.Register(registerDto);
				TempData["Success"] = "User registered successfully";
			}
			catch (Exception e)
			{
				TempData["Error"] = e.Message;
				return View(registerViewModel);
			}
			
			return RedirectToAction(nameof(SendConfirmationEmail), new { email = registerDto.Email });
		}

		
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
				return View(loginViewModel);
			
			var loginDto = new LoginDto(
				loginViewModel.Email,
				loginViewModel.Password
				);

			try
			{
				var loginResult = await authService.Login(loginDto);

				if (loginResult.IsLockedOut)
					throw new AccountLockedException();

				if (!loginResult.Succeeded) 
					return RedirectToAction(nameof(SendConfirmationEmail), new { email = loginDto.Email });

				TempData["Success"] = "You have loggedin successfully";
			}
			catch(Exception ex)
			{
				TempData["Error"] = ex.Message;
				return View(loginViewModel);
			}

			return RedirectToAction("Index", "Home", new { area = "Customer" });
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await authService.Logout();
			return RedirectToAction(nameof(Login));
		}


		[HttpGet]
		public async Task<IActionResult> SendConfirmationEmail(string email)
		{
			await authService.SendConfirmationEmail(email);

			return View();
		}

	
		public async Task<IActionResult> ConfirmAccount(string id, string token)
		{
			var isConfirmed = await authService.ConfirmAccount(id, token);

			if (isConfirmed)
			{
				ViewBag.Message = "Your email has been confirmed successfully!";
				ViewBag.IsSuccess = true;
			}
			else
			{
				ViewBag.Message = "Invalid or expired confirmation link.";
				ViewBag.IsSuccess = false;
			}


			return View();
		}

		// public  IActionResult ForgetPassword()
		// {
		// 	return View();
		// }
		//
		// public IActionResult ConfirmAccount()
		// {
		// 	return Ok();
		// }
		//
		// public IActionResult AccountDetails()
		// {
		// 	return View();
		// }
		//
		// public IActionResult UpdateAccount()
		// {
		// 	return View();
		// }
		//
		// [HttpPost]
		// public IActionResult UpdateAccount(object updatedAccount)
		// {
		// 	return RedirectToAction(nameof(AccountDetails));
		// }
	}
}
