using Bulky.Core.Application.Models.Category;
using Bulky.Core.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
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
			var result = await categoryService.Create(category, cancellationToken);

			if(result.IsSuccess)
			{
				TempData["Success"] = "Category Has Been Created Successfully";
				return RedirectToAction(nameof(Index));
			}

			TempData["Error"] = "An Error has been Occured.";
			return View(category);
			
		}

		// POST: CategoryController/Edit
		[HttpPost]
		public async Task<IActionResult> Edit([FromBody] CategoryDto categoryDto)
		{
		
			var result = await categoryService.Update(categoryDto);
			if(result.IsSuccess)
			{
				TempData["Success"] = "Category has been updated successfully.";
				return Ok();
			}

			TempData["Error"] = "An Error has been Occured.";
			return StatusCode(500);
		}

		// POST: CategoryController/Delete/5
		[HttpPost]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var result = await categoryService.DeleteByIdAsync(id, cancellationToken);

			if(result.IsSuccess)
				TempData["Success"] = "Category has been deleted successfully.";
			else	
				TempData["Error"] = "An Error has been occured.s";
			

			return RedirectToAction("Index");
		}
	}
}
