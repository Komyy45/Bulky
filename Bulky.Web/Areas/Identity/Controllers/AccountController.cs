using Bulky.Core.Application.Models.Identity;
using Bulky.Web.Areas.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bulky.Core.Ports.In;

namespace Bulky.Web.Areas.Identity.Controllers
{
	[Area("Identity")]
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
			
			var result = await authService.Register(registerDto);

			if(result.IsSuccess)
			{
				TempData["Success"] = "User registered successfully";
			}
			else
			{
				foreach (var error in result.Errors!)
					ModelState.AddModelError(error.Code, error.Message);

				TempData["Error"] = "An Error has been Occured";

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

			var result = await authService.Login(loginDto);

			if(result.IsSuccess)
			{
				if(result.Value.Succeeded)
				{
					TempData["Success"] = "You've Logged In Successfully";
					return RedirectToAction("Index", "Home", new { area = "Customer" });
				}
				else if (result.Value.IsLockedOut)
					TempData["Error"] = "Your Account is locked right now, try again later";
				else if (result.Value.IsNotAllowed)
					return RedirectToAction(nameof(SendConfirmationEmail), new { email = loginDto.Email });
				else
					TempData["Error"] = "Invalid Login!";
			}
			else
			{
				foreach (var error in result.Errors!)
					ModelState.AddModelError(error.Code, error.Message);

				TempData["Error"] = "An Error has been Occured";
			}

			return View(loginViewModel);	
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
			var result = await authService.SendConfirmationEmail(email);

			if(result.IsFailure)
			{
				var error = result.Errors!.FirstOrDefault();
			 	TempData["Error"] = error!.Message;
			}

			return View();
		}

	
		public async Task<IActionResult> ConfirmAccount(string id, string token)
		{
			var result = await authService.ConfirmAccount(id, token);

			if (result.IsSuccess)
			{
				ViewBag.Message = "Your email has been confirmed successfully!";
				ViewBag.IsSuccess = true;
			}
			else
			{
				foreach (var error in result.Errors!)
					ModelState.AddModelError(error.Code, error.Message);
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
