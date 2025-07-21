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
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CategoryDto category, CancellationToken cancellationToken)
		{
			try
			{
				await categoryService.Create(category, cancellationToken);
				TempData["Success"] = "Category Has Been Created Successfully";
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				TempData["Error"] = "An Error has been Occured.";
				return View(category);
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
		public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			try
			{
				await categoryService.DeleteByIdAsync(id, cancellationToken);
				TempData["Success"] = "Category has been deleted successfully.";
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				TempData["Error"] = "An Error has been occured.s";
				return View("Index");
			}
		}
	}
}
