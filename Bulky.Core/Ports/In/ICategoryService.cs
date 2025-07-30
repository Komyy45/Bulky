using Bulky.Core.Application.Models.Category;
using Bulky.Core.Application.Models.Common;
using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Ports.In;

public interface ICategoryService
{
	public Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
	public Task<Result> Create(CategoryDto categoryDto, CancellationToken cancellationToken);
	public Task<Result> Update(CategoryDto categoryDto);
	public Task<Result> DeleteByIdAsync(int id, CancellationToken cancellationToken);
}