using Bulky.Core.Application.Models.Category;
using Bulky.Core.Application.Models.Common;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Ports.In;
using Bulky.Core.Ports.Out;

namespace Bulky.Core.Application.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
	private readonly IGenericRepository<Category, int> _categoryRepository = unitOfWork.GetRepository<Category, int>();
	public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
	{
		var categories = await _categoryRepository.GetAll(cancellationToken, true);

		var categoryDtos = categories.Select(c => new CategoryDto(c.Id, c.Name));

		return categoryDtos;
	}

	public async Task<Result> Create(CategoryDto categoryDto, CancellationToken cancellationToken)
	{
		var category = new Category()
		{
			Id = categoryDto.Id,
			Name = categoryDto.Name
		};

		await _categoryRepository.Add(category, cancellationToken);

		var rowsEffected = await unitOfWork.CompleteAsync();

		return rowsEffected > 0 ? Result.Success() : Result.Failure();
	}

	public async Task<Result> Update(CategoryDto categoryDto)
	{
		var category = new Category()
		{
			Id = categoryDto.Id,
			Name = categoryDto.Name
		};

		_categoryRepository.Update(category);

		var rowsEffected = await unitOfWork.CompleteAsync();

		return rowsEffected > 0 ? Result.Success() : Result.Failure();
	}

	public async Task<Result> DeleteByIdAsync(int id, CancellationToken cancellationToken)
	{
		var rowsEffected = await _categoryRepository.Delete(id, cancellationToken);

		return rowsEffected > 0 ? Result.Success() : Result.Failure();
	}
}