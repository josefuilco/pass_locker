using PassLocker.Application.DTOs;
using PassLocker.Application.Service.Interfaces;
using PassLocker.Domain.Entity;
using PassLocker.Domain.Repository;

namespace PassLocker.Application.Service.Implementation;

public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository repository;

	public CategoryService(ICategoryRepository repository)
	{
		this.repository = repository;
	}

	public async Task DeleteCategoryAsync(int categoryId)
	{
		await repository.RemoveCategoryAsync(categoryId);
	}

	public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
	{
		var categories = await repository.GetAllCategoriesAsync();
		return categories.Select(c => new CategoryDTO(c.Id, c.Name ?? "Unknown"));
	}

	public async Task SaveCategoryAsync(Category category)
	{
		await repository.SaveCategoryAsync(category);
	}

	public async Task UpdateCategoryAsync(Category category)
	{
		await repository.UpdateCategoryAsync(category);
	}
}