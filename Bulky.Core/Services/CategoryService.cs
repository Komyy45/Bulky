using Bulky.Core.Contracts.Ports.Repositories;
using Bulky.Core.Contracts.Services;
using Bulky.Core.Entities;
using Bulky.Core.Models.Category;

namespace Bulky.Core.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IGenericRepository<Category, int> _categoryRepository = unitOfWork.GetRepository<Category, int>();


    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categoryRepository = unitOfWork.GetRepository<Category, int>();

        var categories = await categoryRepository.GetAll(cancellationToken, true);

        var categoryDtos = categories.Select(c => new CategoryDto(c.Id, c.Name));

        return categoryDtos;
    }
    
    public async Task Create(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = new Category()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };

        await _categoryRepository.Add(category, cancellationToken);

        await unitOfWork.CompleteAsync();
    }

    public async Task Update(CategoryDto categoryDto)
    {
        var category = new Category()
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };
        
        _categoryRepository.Update(category);

        await unitOfWork.CompleteAsync();
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.Delete(id, cancellationToken);
    }
}