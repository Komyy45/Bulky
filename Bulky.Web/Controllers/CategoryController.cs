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

		// POST: CategoryController/Edit
		[HttpPost]
		public async Task<IActionResult> Edit([FromBody] CategoryDto categoryDto)
		{
			try
			{
				await categoryService.Update(categoryDto);
				TempData["Success"] = "Category has been updated successfully.";
				return Ok();
			}
			catch
			{
				TempData["Error"] = "An Error has been Occured.";
				return StatusCode(500);
			}
			
		}

		// POST: CategoryController/Delete/5
		[HttpPost]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			try
			{
				await categoryService.DeleteByIdAsync(id, cancellationToken);
				TempData["Success"] = "Category has been deleted successfully.";
			}
			catch (Exception e)
			{
				TempData["Error"] = "An Error has been occured.s";
			}
			
			return RedirectToAction("Index");
		}
	}
}
