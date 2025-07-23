using Bulky.Core.Contracts.Services;
using Bulky.Core.Models;
using Bulky.Core.Models.Product;
using Bulky.Web.Models;
using Bulky.Web.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    // GET
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        //var products = await productService.GetAllAsync(cancellationToken);

        //var productViewModels = products.Select(e => new ProductViewModel()
        //{
        //   Id = e.Id,
        //   Title = e.Title,
        //   Description = e.Description,
        //   Author = e.Author,
        //   ISBN = e.ISBN,
        //   Price = e.Price,
        //   Category = e.Category
        //});
        
        return View();
    }

	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
       var product = await productService.GetAsync(id, cancellationToken);

        var productDetailsViewModel = new ProductDetailsViewModel()
        {
            Id = product.Id,
            Title = product.Title,
            Author = product.Author,
            Description = product.Description,
            ISBN = product.ISBN,
            Price = product.Price,
            CategoryId = product.CategoryId,
            PictureUrl = null!
        };

       return View(productDetailsViewModel);
    }

	// GET
	public IActionResult Create()
    {
        return View();
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateEditViewModel product, CancellationToken cancellationToken)
    {
        var productDto = new ProductDetailsDto(
            product.Id,
            product.Title,
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
        var productViewModel = new ProductCreateEditViewModel()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            ISBN = product.ISBN,
            CategoryId = product.CategoryId,
            Price = product.Price,
            Author = product.Author
        };
        return View(productViewModel);
    }
    
    // POST
    [HttpPost]
    public async Task<IActionResult> Edit(ProductCreateEditViewModel product)
    {
        var productDto = new ProductDetailsDto(
            product.Id,
            product.Title,
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