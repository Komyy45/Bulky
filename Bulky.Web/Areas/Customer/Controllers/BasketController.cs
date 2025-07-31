using System.Security.Claims;
using Bulky.Core.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Areas.Customer.Controllers
{
	[Authorize]
	[Area("Customer")]
	public class BasketController : Controller
	{
		private readonly IBasketService basketService;

		public BasketController(IBasketService basketService)
		{
			this.basketService = basketService;
		}

		public async Task<IActionResult> Index()
		{

			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var result = await basketService.Get(userId);

			return View(result.Value);
		}

		public async Task<IActionResult> Add(int id)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await basketService.Add(id, userId);

			return View(nameof(Index), result.Value);
		}

		public async Task<IActionResult> Remove(int id)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var result = await basketService.Remove(id, userId);

			return View(nameof(Index), result.Value);
		}

		[HttpPost]		
		public async Task<ActionResult<bool>> DeleteBasket(string id) 
		{
			var result = await basketService.Delete(id);

			if (result.IsFailure) TempData["Error"] = "An Error has been occured";
			else TempData["Success"] = "Cart has been reset";

			return Ok(result.IsSuccess);
		}
	}
}
