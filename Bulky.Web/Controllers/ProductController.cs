using Bulky.Core.Contracts.Services;
using Bulky.Core.Models.Common;
using Bulky.Core.Models.Product;
using Bulky.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Controllers;

public class ProductController(IConfiguration configuration, IProductService productService) : Controller
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
        ViewBag.configuration = configuration;
       var product = await productService.GetAsync(id, cancellationToken);

        if (product is null) return NotFound();

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
	public IActionResult Create()
    {
		ViewBag.configuration = configuration;
		return View();
    }
    
    // POST
    [HttpPost]
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

        try
        {
            await productService.CreateAsync(productDto, cancellationToken);
            TempData["Success"] = "Product has been updated Successfully.";
        }
        catch (Exception e)
        {
            TempData["Error"] = "An Error has been Occured.";
            return View(product);
        }
        
        return RedirectToAction(nameof(Index));
    } 
    
    
    // GET
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
		var product = await productService.GetAsync(id, cancellationToken);

        if (product is null) return NotFound();

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
    public async Task<IActionResult> Edit(ProductCreateEditViewModel product)
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


		try
		{
            await productService.UpdateAsync(productDto);
            TempData["Success"] = "Product has been updated Successfully.";
        }
        catch (Exception e)
        {
            TempData["Error"] = "An Error has been Occured.";
            return View(product);
        }
        
        return RedirectToAction(nameof(Index));
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await productService.DeleteAsync(id, cancellationToken);
            TempData["Success"] = "Product has been deleted Successfully.";
        }
        catch (Exception e)
        {
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