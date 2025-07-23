using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Bulky.Core.Contracts;
using Bulky.Core.Contracts.Services;
using Bulky.Core.Entities;
using Bulky.Core.Models;
using Bulky.Core.Models.Product;
using Bulky.Core.Specification.Products;
using Bulky.Web.Models;

namespace Bulky.Core.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IGenericRepository<Product, int> _productsRepository = unitOfWork.GetRepository<Product, int>(); 
    
    public async Task<DataTableViewModel<ProductDto>> GetAllAsync(DataTableRequest request, CancellationToken cancellationToken)
    {
		Expression<Func<Product, bool>> filter = p => true;
		if (!string.IsNullOrWhiteSpace(request.Search?.Value))
		{
			var searchValue = request.Search.Value.Trim().ToLower();
			filter = p =>
				p.Title.ToLower().Contains(searchValue) ||
				p.Description.ToLower().Contains(searchValue) ||
				p.Author.ToLower().Contains(searchValue) ||
				p.ISBN.ToLower().Contains(searchValue);
		}

        var totalCount = await _productsRepository.CountAsync(cancellationToken);

		var spec = new GetAllProductsSpecification(filter, request.Start, request.Length);
        var products = await _productsRepository.GetAll(spec, cancellationToken, true);

        var productDtos = products.Select(e => new ProductDto(
            e.Id,
            e.Title,
            e.Description,
            e.Author,
            e.ISBN,
            e.Price,
            e.Category.Name
        ));

        return new DataTableViewModel<ProductDto>()
        {
            Draw = request.Draw,
            RecordsTotal = totalCount,
            RecordsFiltered = string.IsNullOrWhiteSpace(request.Search?.Value) ? totalCount : productDtos.Count(),
            Data = productDtos,
        };
    }

    public async Task<ProductDetailsDto> GetAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.Get(id, cancellationToken);

        return new ProductDetailsDto(
            product.Id,
            product.Title,
            product.Description,
            product.Author,
            product.ISBN,
            product.Price,
            product.CategoryId
        );
    }

    public async Task UpdateAsync(ProductDetailsDto product)
    {
        var updatedProduct = new Product()
        {
            Title = product.Title,
            Description = product.Description,
            Author = product.Author,
            ISBN = product.ISBN,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
        
        _productsRepository.Update(updatedProduct);

        await unitOfWork.CompleteAsync();
    }

    public async Task CreateAsync(ProductDetailsDto product, CancellationToken cancellationToken)
    {
        var updatedProduct = new Product()
        {
            Title = product.Title,
            Description = product.Description,
            Author = product.Author,
            ISBN = product.ISBN,
            Price = product.Price,
            CategoryId = product.CategoryId
        };
        
        await _productsRepository.Add(updatedProduct, cancellationToken);

        await unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _productsRepository.Delete(id, cancellationToken);
    }
}