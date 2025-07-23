using Bulky.Core.Models;
using Bulky.Core.Models.Product;
using Bulky.Web.Models;

namespace Bulky.Core.Contracts.Services;

public interface IProductService
{
    public Task<DataTableViewModel<ProductDto>> GetAllAsync(DataTableRequest request, CancellationToken cancellationToken);
    public Task<ProductDetailsDto> GetAsync(int id, CancellationToken cancellationToken);
    public Task CreateAsync(ProductDetailsDto product, CancellationToken cancellationToken);
    public Task UpdateAsync(ProductDetailsDto product);
    public Task DeleteAsync(int id, CancellationToken cancellationToken);
}