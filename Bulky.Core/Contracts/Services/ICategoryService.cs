using Bulky.Core.Entities;
using Bulky.Core.Models.Category;

namespace Bulky.Core.Contracts.Services;

public interface ICategoryService
{
    public Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task Create(CategoryDto categoryDto, CancellationToken cancellationToken);
    public Task Update(CategoryDto categoryDto);
    public Task DeleteByIdAsync(int id, CancellationToken cancellationToken);
}