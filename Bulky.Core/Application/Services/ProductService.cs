using System.Linq.Expressions;
using System.Text;
using Bulky.Core.Application.Models.Common;
using Bulky.Core.Application.Models.Product;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Domain.Specification.Products;
using Bulky.Core.Ports.In;
using Bulky.Core.Ports.Out;

namespace Bulky.Core.Application.Services;

public class ProductService(IUnitOfWork unitOfWork, IBlobStorage blobStorage) : IProductService
{
	private readonly IGenericRepository<Product, int> _productsRepository = unitOfWork.GetRepository<Product, int>();

	public async Task<DataTableViewModel<ProductDto>> GetAllAsync(DataTableRequest request, CancellationToken cancellationToken)
	{
		Expression<Func<Product, bool>> filter = p => true;
		var isSearching = string.IsNullOrWhiteSpace(request.Search?.Value);

		if (!isSearching)
		{
			var searchValue = request.Search!.Value.Trim().ToLower();
			filter = p =>
				p.Title.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
				p.Description.ToLower().Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
				p.Author.ToLower().Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
				p.ISBN.ToLower().Contains(searchValue, StringComparison.OrdinalIgnoreCase);
		}

		var totalCount = await _productsRepository.CountAsync(cancellationToken);

		var spec = new GetAllProductsSpecification(filter, request.Start, request.Length);

		var products = await _productsRepository.GetAll(spec, cancellationToken, true);

		var productDtos = products.Select(e => new ProductDto(
			e.Id,
			e.Title,
			e.Author,
			e.ISBN,
			e.Price,
			e.Category.Name
		));

		return new DataTableViewModel<ProductDto>()
		{
			Draw = request.Draw,
			RecordsTotal = totalCount,
			RecordsFiltered = isSearching ? totalCount : productDtos.Count(),
			Data = productDtos,
		};
	}

	public async Task<Result<ProductDetailsDto>> GetAsync(int id, CancellationToken cancellationToken)
	{
		var product = await _productsRepository.Get(id, cancellationToken);

		if (product is null)
			return Result<ProductDetailsDto>.Failure(Error.NotFound("Product Not Found"));

		var stream = await blobStorage.DownloadAsync("descriptions", product.Description);

		using var streamReader = new StreamReader(stream);

		var description = await streamReader.ReadToEndAsync();

		return Result<ProductDetailsDto>.Success(new ProductDetailsDto(
			product.Id,
			product.Title,
			product.Picture,
			description,
			product.Author,
			product.ISBN,
			product.Price,
			product.CategoryId
		));
	}

	public async Task<Result<bool>> UpdateAsync(ProductCreateEditDto product)
	{
		var existingProduct = await _productsRepository.Get(product.Id, CancellationToken.None);

		if (existingProduct is null) Result<bool>.Failure(Error.NotFound("Product Not Found!"));

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(product.Description));

		var descriptionUrl = await blobStorage.UploadAsync(stream, "descriptions", existingProduct!.Description);

		if (product.Picture is not null)
		{
			if (product.ExistingPictureUrl is not null)
			{
				await blobStorage.UploadAsync(product.Picture.OpenReadStream(), "images", product.ExistingPictureUrl);
			}

			existingProduct.Picture = await blobStorage.UploadAsync(product.Picture, "images");
		}

		existingProduct.Id = product.Id;
		existingProduct.Title = product.Title;
		existingProduct.Description = descriptionUrl;
		existingProduct.Author = product.Author;
		existingProduct.ISBN = product.ISBN;
		existingProduct.Price = product.Price;
		existingProduct.CategoryId = product.CategoryId;

		_productsRepository.Update(existingProduct);

		var rowsEffected = await unitOfWork.CompleteAsync();

		return Result<bool>.Success(rowsEffected > 0);
	}

	public async Task<Result<bool>> CreateAsync(ProductCreateEditDto product, CancellationToken cancellationToken)
	{
		var createdProduct = new Product()
		{
			Title = product.Title,
			Author = product.Author,
			ISBN = product.ISBN,
			Price = product.Price,
			CategoryId = product.CategoryId,
		};

		using var stream = new MemoryStream(Encoding.UTF8.GetBytes(product.Description));

		createdProduct.Description = await blobStorage.UploadAsync(stream, "descriptions", $"{Guid.NewGuid()}.html");

		if (product.Picture is not null)
		{
			createdProduct.Picture = await blobStorage.UploadAsync(product.Picture, "images");
		}

		await _productsRepository.Add(createdProduct, cancellationToken);

		var rowsEffected = await unitOfWork.CompleteAsync();

		return Result<bool>.Success(rowsEffected > 0);
	}

	public async Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
	{
		var rowsEffected = await _productsRepository.Delete(id, cancellationToken);
		return Result<bool>.Success(rowsEffected > 0);
	}
}