using Bulky.Core.Models.Common;
using Bulky.Core.Models.Product;

namespace Bulky.Core.Contracts.Services;

public interface IProductService
{
    public Task<DataTableViewModel<ProductDto>> GetAllAsync(DataTableRequest request, CancellationToken cancellationToken);
    public Task<ProductDetailsDto> GetAsync(int id, CancellationToken cancellationToken);
    public Task CreateAsync(ProductCreateEditDto product, CancellationToken cancellationToken);
    public Task UpdateAsync(ProductCreateEditDto product);
    public Task DeleteAsync(int id, CancellationToken cancellationToken);
}