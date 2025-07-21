using Bulky.Core.Contracts.Services;
using Bulky.Core.Models.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Controllers
{
	public class CategoryController(ICategoryService categoryService) : Controller
	{
		// GET: CategoryController
		public async Task<ActionResult> Index(CancellationToken cancellationToken)
		{
			var categories = await categoryService.GetAllAsync(cancellationToken);
			return View(categories);
		}

		// GET: CategoryController/Create
		public ActionResult Create(CategoryDto categoryDto)
		{
			return View();
		}

		// POST: CategoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoryController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CategoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(CategoryDto categoryDto)
		{

			categoryService.Update(categoryDto);
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoryController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}
	}
}
