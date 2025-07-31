using Bulky.Core.Application.Models.Common;
using Bulky.Core.Application.Models.Product;
using Bulky.Core.Ports.In;
using Bulky.Web.Areas.Admin.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IProductService productService) : Controller
{
	// GET : Product/Index
	[HttpGet]
	public IActionResult Index(CancellationToken cancellationToken)
	{
		return View();
	}


	// GET : Product/Details/{id}
	[HttpGet]
	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
	{
		var result = await productService.GetAsync(id, cancellationToken);

		if(result.IsFailure)
		{
			return RedirectToAction("Error", "Home", new { area = "Customer" });
		}

		var product = result.Value;

		var productDetailsViewModel = new ProductDetailsViewModel()
		{
			Id = product.Id,
			Title = product.Title,
			Author = product.Author,
			Description = product.Description,
			ISBN = product.ISBN,
			Price = product.Price,
			CategoryId = product.CategoryId,
			PictureUrl = product.Picture!
		};

		return View(productDetailsViewModel);
	}

	// GET
	[Authorize(Roles = "Admin")]
	public IActionResult Create()
	{
		return View();
	}

	// POST
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create(ProductCreateEditViewModel product, CancellationToken cancellationToken)
	{
		var productDto = new ProductCreateEditDto(
			product.Id,
			product.Title,
			product.ExistingPicture,
			product.Picture,
			product.Description,
			product.Author,
			product.ISBN,
			product.Price,
			product.CategoryId
		);

		var result = await productService.CreateAsync(productDto, cancellationToken);

		if(result.IsSuccess)
		{
			if(result.Value)
				TempData["Success"] = "Product has been updated Successfully.";
			else
				TempData["Error"] = String.Join(" ", result.Errors!.Select(e => e.Message));
		}

		return RedirectToAction(nameof(Index));
	}


	// GET
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
	{
		var result = await productService.GetAsync(id, cancellationToken);


		if(result.IsFailure)
		{
			return RedirectToAction("PageNotFound", "Home", new { area = "Customer" });
		}

		var product = result.Value;

		var productViewModel = new ProductCreateEditViewModel()
		{
			Id = product.Id,
			Title = product.Title,
			Description = product.Description,
			ISBN = product.ISBN,
			CategoryId = product.CategoryId,
			Price = product.Price,
			Author = product.Author,
			ExistingPicture = product.Picture
		};

		return View(productViewModel);
	}

	// POST
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Edit(ProductCreateEditViewModel product)
	{
		if (!ModelState.IsValid) return View(product);


		var productDto = new ProductCreateEditDto(
			product.Id,
			product.Title,
			product.ExistingPicture,
			product.Picture,
			product.Description,
			product.Author,
			product.ISBN,
			product.Price,
			product.CategoryId
		);
		
		var result = await productService.UpdateAsync(productDto);

		if(result.IsSuccess)
		{
			if(result.Value)
				TempData["Success"] = "Product has been updated Successfully.";
			else
				TempData["Error"] = "An Error has been Occured.";
		}


		return RedirectToAction(nameof(Index));
	}

	// POST
	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
	{
		var result = await productService.DeleteAsync(id, cancellationToken);

		if (result.IsSuccess)
		{
			if (result.Value)
				TempData["Success"] = "Product has been deleted Successfully.";
			else
				TempData["Error"] = "An Error has been Occured.";
		}

		return RedirectToAction(nameof(Index));
	}

	[HttpPost]
	public async Task<ActionResult<DataTableViewModel<ProductViewModel>>> GetAll(DataTableRequest request, CancellationToken cancellationToken)
	{

		var products = await productService.GetAllAsync(request, cancellationToken);

		return Ok(products);
	}
}