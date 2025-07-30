using Bulky.Core.Application.Models.Common;
using Bulky.Core.Application.Models.Product;

namespace Bulky.Core.Ports.In;

public interface IProductService
{
	public Task<DataTableViewModel<ProductDto>> GetAllAsync(DataTableRequest request, CancellationToken cancellationToken);
	public Task<Result<ProductDetailsDto>> GetAsync(int id, CancellationToken cancellationToken);
	public Task<Result<bool>> CreateAsync(ProductCreateEditDto product, CancellationToken cancellationToken);
	public Task<Result<bool>> UpdateAsync(ProductCreateEditDto product);
	public Task<Result<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
}